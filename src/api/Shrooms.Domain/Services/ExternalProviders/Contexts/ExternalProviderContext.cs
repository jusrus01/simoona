using Shrooms.Domain.Services.ExternalProviders.Strategies;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Contexts
{
    public class ExternalProviderContext : IExternalProviderContext
    {
        private IExternalProviderStrategy _strategy;

        public async Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            return await _strategy.ExecuteStrategyAsync();
        }

        public void SetStrategy(IExternalProviderStrategy providerStrategy)
        {
            _strategy = providerStrategy;
        }
    }
}
