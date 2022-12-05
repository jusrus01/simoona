using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Users;

namespace Shrooms.Domain.Services.ExternalProviders.Arguments
{
    public record LoginArgs(ExternalLoginInfo LoginInfo, ExternalLoginRequestDto Request);
}
