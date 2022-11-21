using Microsoft.AspNetCore.Identity;
using Shrooms.DataLayer.EntityModels.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Users
{
    public interface IApplicationUserManager
    {
        Task<ApplicationUser> FindByIdAsync(string id);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<ApplicationUser> FindByNameAsync(string userName);

        Task<ApplicationUser> RestoreSoftDeletedUserByIdAsync(string id);

        Task<ApplicationUser> RestoreSoftDeletedUserByEmailAsync(string email);

        Task CheckPasswordAsync(ApplicationUser user, string password);

        Task<IList<string>> GetRolesAsync(ApplicationUser user);

        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);

        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);

        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        Task ResetPasswordAsync(ApplicationUser user, string token, string newPassword);

        Task AddLoginAsync(ApplicationUser user, ExternalLoginInfo externalLoginInfo);

        Task AddLoginAsync(ApplicationUser user, UserLoginInfo userLoginInfo);

        Task ConfirmEmailAsync(ApplicationUser user, string code);

        Task CreateAsync(ApplicationUser user, string password);

        Task CreateAsync(ApplicationUser user);

        Task<bool> HasExternalLoginAsync(ApplicationUser user);

        Task RemovePasswordAsync(ApplicationUser user);

        Task AddPasswordAsync(ApplicationUser user, string password);

        Task RemoveLoginAsync(ApplicationUser user, string provider, string providerKey);

        Task UpdateAsync(ApplicationUser user);

        Task<bool> UserExistsAsync(string email);

        Task<bool> IsUserSoftDeletedAsync(string email);
    }
}
