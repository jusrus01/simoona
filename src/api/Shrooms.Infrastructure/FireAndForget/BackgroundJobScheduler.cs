using Shrooms.Contracts.Infrastructure.FireAndForget;
using System;
using System.Threading.Tasks;

namespace Shrooms.Infrastructure.FireAndForget
{
    public class BackgroundJobScheduler : IBackgroundJobScheduler
    {
        private readonly IBackgroundJobQueue _jobQueue;
        private readonly ITenantNameContainer _tenantNameContainer;

        public BackgroundJobScheduler(ITenantNameContainer tenantNameContainer, IBackgroundJobQueue jobQueue)
        {
            _tenantNameContainer = tenantNameContainer;
            _jobQueue = jobQueue;
        }

        public void EnqueueJob<TService>(Func<TService, Task> serviceMethod)
        {
            var job = new BackgroundJob(typeof(TService), new Func<object, Task>(o => serviceMethod((TService)o)), _tenantNameContainer.TenantName);

            _jobQueue.EnqueueJob(job);
        }
    }
}
