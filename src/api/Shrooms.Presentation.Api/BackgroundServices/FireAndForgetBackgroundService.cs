using Autofac;
using Autofac.Core.Lifetime;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Infrastructure.FireAndForget;
using Shrooms.Contracts.Options;
using Shrooms.Infrastructure.FireAndForget;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.BackgroundServices
{
    public class FireAndForgetBackgroundService : BackgroundService//TODO: read backgorund service
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IFireAndForgetJobQueue _jobQueue;

        private readonly ApplicationOptions _applicationOptions;

        public FireAndForgetBackgroundService(
            IOptions<ApplicationOptions> applicationOptions,
            ILifetimeScope lifetimeScope,
            IFireAndForgetJobQueue jobQueue)
        {
            _applicationOptions = applicationOptions.Value;

            _lifetimeScope = lifetimeScope;
            _jobQueue = jobQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = _jobQueue.DequeueJob();

                if (job == null)
                {
                    await Task.Delay(10000, stoppingToken);

                    continue;
                }

                ILifetimeScope? scope = null;

                try
                {
                    scope = _lifetimeScope.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag, builder =>
                    {
                        builder.RegisterInstance(new TenantNameContainer(job.TenantName))// TODO: Test this
                            .As<ITenantNameContainer>()
                            .SingleInstance();
                    });

                    var callerService = scope.Resolve(job.ServiceType);

                    await job.ServiceMethod(callerService);
                }
                catch (Exception e)
                {
                    // TODO: Log error
                }
                finally
                {
                    scope?.Dispose();
                }
            }
        }
    }
}
