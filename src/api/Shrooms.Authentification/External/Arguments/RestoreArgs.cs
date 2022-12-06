using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.DataLayer.EntityModels.Models;
using System.Security.Claims;

namespace Shrooms.Authentication.External.Arguments
{
    public record RestoreArgs(
        ExternalLoginInfo LoginInfo,
        ExternalLoginRequestDto Request,
        ApplicationUser RestorableUser,
        ClaimsIdentity Identity,
        int OrganizationId);
}