using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Authentication.External.Arguments
{
    public record RegisterArgs(ExternalLoginInfo LoginInfo, ExternalLoginRequestDto Request, Organization Organization);
}
