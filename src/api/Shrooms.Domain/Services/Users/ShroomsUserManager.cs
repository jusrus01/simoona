using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.ServiceValidators.Validators.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Users
{
    public class ShroomsUserManager : IShroomsUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserManagerValidator _validator;

        public ShroomsUserManager(UserManager<ApplicationUser> userManager, IUserManagerValidator validator)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public async Task CheckPasswordAsync(ApplicationUser user, string password)
        {
            await _validator.CheckPasswordAsync(user, password);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await FindUserByAsync(_userManager.FindByEmailAsync, email);
        }

        public async Task<ApplicationUser> FindByIdAsync(string id)
        {
            return await FindUserByAsync(_userManager.FindByIdAsync, id);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await FindUserByAsync(_userManager.FindByNameAsync, userName);
        }

        private async Task<ApplicationUser> FindUserByAsync(Func<string, Task<ApplicationUser>> findFunction, string param)
        {
            var user = await findFunction(param);

            _validator.CheckIfUserExists(user);

            return user;
        }

        public Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public async Task AddLoginAsync(ApplicationUser user, ExternalLoginInfo externalLoginInfo)
        {
            var result = await _userManager.AddLoginAsync(user, externalLoginInfo);

            _validator.CheckIfLoginWasAdded(result);
        }

        public async Task AddLoginAsync(ApplicationUser user, UserLoginInfo userLoginInfo)
        {
            var result = await _userManager.AddLoginAsync(user, userLoginInfo);

            _validator.CheckIfLoginWasAdded(result);
        }

        public async Task ConfirmEmailAsync(ApplicationUser user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);

            _validator.CheckIfEmailWasConfirmed(result);
        }

        public async Task AddToRoleAsync(ApplicationUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task CreateAsync(ApplicationUser user, string password)
        {
            var identityResult = await _userManager.CreateAsync(user, password);

            _validator.CheckIfUserWasCreated(identityResult);
        }

        public async Task<bool> HasExternalLoginAsync(ApplicationUser user)
        {
            var logins = await _userManager.GetLoginsAsync(user);

            return logins.Any(login => login.ProviderDisplayName != AuthenticationConstants.InternalLoginProvider);
        }

        public async Task RemovePasswordAsync(ApplicationUser user)
        {
            await _userManager.RemovePasswordAsync(user);
        }

        public async Task AddPasswordAsync(ApplicationUser user, string password)
        {
            await _userManager.AddPasswordAsync(user, password);
        }
    }
}
