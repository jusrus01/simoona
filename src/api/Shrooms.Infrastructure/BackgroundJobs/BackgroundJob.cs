using Shrooms.Contracts.Infrastructure.FireAndForget;
using System;
using System.Threading.Tasks;

namespace Shrooms.Infrastructure.BackgroundJobs
{
    public class BackgroundJob : IBackgroundJob
    {
        public Type ServiceType { get; }

        public Func<object, Task> ServiceMethod { get; }

        public string TenantName { get; }

        public BackgroundJob(Type serviceType, Func<object, Task> serviceMethod, string tenantName)
        {
            ServiceType = serviceType;
            ServiceMethod = serviceMethod;
            TenantName = tenantName;
        }
    }
}
