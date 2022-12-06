using Shrooms.Authentication.External.Strategies;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using System.Threading.Tasks;

namespace Shrooms.Authentication.External
{
    public interface IExternalProviderStrategyFactory
    {
        public Task<IExternalProviderStrategy> GetStrategyAsync(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto);

        public IExternalProviderStrategy GetStrategy(ExternalProviderPartialResult partialResult);
    }
}
