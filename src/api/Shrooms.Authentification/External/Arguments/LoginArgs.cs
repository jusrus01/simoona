using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Users;

namespace Shrooms.Authentication.External.Arguments
{
    public record LoginArgs(ExternalLoginInfo LoginInfo, ExternalLoginRequestDto Request);
}
