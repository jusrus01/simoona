using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Users;

namespace Shrooms.Authentication.External.Arguments
{
    public record LinkingArgs(ExternalLoginInfo LoginInfo, ExternalLoginRequestDto Request, bool? RestoreUser);
}
