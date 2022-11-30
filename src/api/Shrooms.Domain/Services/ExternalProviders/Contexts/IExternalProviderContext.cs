using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.ExternalProviders;
using Shrooms.Domain.Services.ExternalProviders.Strategies;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Contexts
{
    public interface IExternalProviderContext
    {
        void SetStrategy(IExternalProviderStrategy providerStrategy);

        Task<ExternalProviderResult> ExecuteStrategyAsync(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null);
    }
}
