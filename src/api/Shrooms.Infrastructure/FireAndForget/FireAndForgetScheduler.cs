using Shrooms.Contracts.Infrastructure.FireAndForget;
using System;
using System.Threading.Tasks;

namespace Shrooms.Infrastructure.FireAndForget
{
    public class FireAndForgetScheduler : IFireAndForgetScheduler
    {
        private readonly IFireAndForgetJobQueue _jobQueue;
        private readonly ITenantNameContainer _tenantNameContainer;

        public FireAndForgetScheduler(ITenantNameContainer tenantNameContainer, IFireAndForgetJobQueue jobQueue)
        {
            _tenantNameContainer = tenantNameContainer;
            _jobQueue = jobQueue;
        }

        public void EnqueueJob<TService>(Func<TService, Task> serviceMethod)
        {
            var job = new FireAndForgetJob(typeof(TService), new Func<object, Task>(o => serviceMethod((TService)o)), _tenantNameContainer.TenantName);

            _jobQueue.EnqueueJob(job);
        }
    }
}
