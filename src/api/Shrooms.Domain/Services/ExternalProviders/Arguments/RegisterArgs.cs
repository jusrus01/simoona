using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Domain.Services.ExternalProviders.Arguments
{
    public record RegisterArgs(ExternalLoginInfo LoginInfo, ExternalLoginRequestDto Request, Organization Organization);
}
