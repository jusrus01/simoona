using Autofac;
using Autofac.Core.Lifetime;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Infrastructure.FireAndForget;
using Shrooms.Contracts.Options;
using Shrooms.Infrastructure.FireAndForget;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.BackgroundServices
{
    public class FireAndForgetBackgroundService : BackgroundService
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IFireAndForgetJobQueue _jobQueue;
        private readonly ILogger _logger;
        private readonly ApplicationOptions _applicationOptions;

        public FireAndForgetBackgroundService(
            IOptions<ApplicationOptions> applicationOptions,
            ILifetimeScope lifetimeScope,
            IFireAndForgetJobQueue jobQueue,
            ILogger logger)
        {
            _applicationOptions = applicationOptions.Value;
            _logger = logger;
            _lifetimeScope = lifetimeScope;
            _jobQueue = jobQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!_jobQueue.TryDequeueJob(out var job))
                {
                    await Task.Delay(_applicationOptions.WaitBeforeEachBackgroundJobMilliseconds, stoppingToken);

                    continue;
                }

                ILifetimeScope? scope = null;

                try
                {
                    scope = _lifetimeScope.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag, builder =>
                    {
                        builder.RegisterInstance(new TenantNameContainer(job.TenantName))
                            .As<ITenantNameContainer>()
                            .SingleInstance();
                    });

                    var callerService = scope.Resolve(job.ServiceType);

                    await job.ServiceMethod(callerService);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                }
                finally
                {
                    scope?.Dispose();
                }
            }
        }
    }
}
