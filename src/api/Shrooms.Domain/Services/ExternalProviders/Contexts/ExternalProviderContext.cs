using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.ExternalProviders;
using Shrooms.Domain.Services.ExternalProviders.Strategies;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Contexts
{
    public class ExternalProviderContext : IExternalProviderContext
    {
        private IExternalProviderStrategy _strategy;

        public async Task<ExternalProviderResult> ExecuteStrategyAsync(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null)
        {
            return await _strategy.ExecuteStrategyAsync(parameters, loginInfo);
        }

        public void SetStrategy(IExternalProviderStrategy providerStrategy)
        {
            _strategy = providerStrategy;
        }
    }
}
