using Microsoft.AspNetCore.Identity;

namespace Shrooms.Authentication.External.Arguments
{
    public record LoginArgs(ExternalLoginInfo LoginInfo);
}
