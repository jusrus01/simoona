using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Domain.Services.ExternalProviders.Strategies;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public interface IExternalProviderStrategyFactory
    {
        public Task<IExternalProviderStrategy> GetStrategyAsync(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto);

        public IExternalProviderStrategy GetStrategy(ExternalProviderPartialResult partialResult);
    }
}
