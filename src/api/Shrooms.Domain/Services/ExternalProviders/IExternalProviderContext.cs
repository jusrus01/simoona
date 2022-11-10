using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public interface IExternalProviderContext
    {
        void SetStrategy(IExternalProviderStrategy providerStrategy);

        Task<ExternalProviderResult> ExecuteStrategyAsync();
    }
}
