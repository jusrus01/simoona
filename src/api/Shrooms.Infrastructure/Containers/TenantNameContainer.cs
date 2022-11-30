using Shrooms.Contracts.Infrastructure;

namespace Shrooms.Infrastructure.Containers
{
    public class TenantNameContainer : ITenantNameContainer
    {
        public string TenantName { get; }

        public TenantNameContainer(string tenantName)
        {
            TenantName = tenantName;
        }
    }
}
