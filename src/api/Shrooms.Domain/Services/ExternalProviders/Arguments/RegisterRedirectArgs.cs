using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;

namespace Shrooms.Domain.Services.ExternalProviders.Arguments
{
    public record RegisterRedirectArgs(ExternalLoginRequestDto Request, ControllerRouteDto Route);
}
