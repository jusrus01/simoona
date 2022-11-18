using Shrooms.Contracts.Infrastructure.FireAndForget;
using System;
using System.Threading.Tasks;

namespace Shrooms.Infrastructure.FireAndForget
{
    public class FireAndForgetJob : IFireAndForgetJob
    {
        public Type ServiceType { get; }

        public Func<object, Task> ServiceMethod { get; }

        public string TenantName { get; }

        public FireAndForgetJob(Type serviceType, Func<object, Task> serviceMethod, string tenantName)
        {
            ServiceType = serviceType;
            ServiceMethod = serviceMethod;
            TenantName = tenantName;
        }
    }
}
