using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Users;

namespace Shrooms.Domain.Services.ExternalProviders.Arguments
{
    public record LinkingArgs(ExternalLoginInfo LoginInfo, ExternalLoginRequestDto Request, bool? RestoreUser);
}
