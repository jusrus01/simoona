namespace Shrooms.Infrastructure.FireAndForget
{
    // Replacing TenantContainer inplementation in middleware...
    public class TenantNameContainer : ITenantNameContainer
    {
        public string TenantName { get; }

        public TenantNameContainer(string tenantName)
        {
            TenantName = tenantName;
        }
    }

    public interface ITenantNameContainer
    {
        string TenantName { get; }
    }
}
