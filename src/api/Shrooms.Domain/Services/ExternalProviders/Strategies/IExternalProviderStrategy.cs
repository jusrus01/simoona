using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.ExternalProviders;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public interface IExternalProviderStrategy
    {
        Task<ExternalProviderResult> ExecuteStrategyAsync(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null);
    }
}
