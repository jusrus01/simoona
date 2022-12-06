using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;

namespace Shrooms.Authentication.External.Arguments
{
    public record LoginRedirectArgs(ExternalLoginRequestDto Request, ControllerRouteDto Route);
}
