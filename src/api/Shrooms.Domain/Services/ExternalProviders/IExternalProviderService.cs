using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public interface IExternalProviderService
    {
        Task<ExternalProviderResult> ExternalLoginOrRegisterAsync(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto);
        
        Task<IEnumerable<ExternalLoginDto>> GetExternalLoginsAsync(ControllerRouteDto redirectRouteDto, string returnUrl, string userId);
    }
}
