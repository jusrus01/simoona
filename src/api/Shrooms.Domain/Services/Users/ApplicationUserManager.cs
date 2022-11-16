using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DAL;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.ServiceValidators.Validators.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Users
{
    public class ApplicationUserManager : IApplicationUserManager// Q: strict user manager for exceptions?
    {//TODO: figure out wether or not to add exceptions on all Task functions
        private readonly IApplicationUserManagerValidator _validator;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DbSet<ApplicationUser> _usersDbSet;

        public ApplicationUserManager(
            UserManager<ApplicationUser> userManager,
            IApplicationUserManagerValidator validator,
            IUnitOfWork2 uow)
        {
            _usersDbSet = uow.GetDbSet<ApplicationUser>();

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

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
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

        public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task CreateAsync(ApplicationUser user)
        {
            var identityResult = await _userManager.CreateAsync(user);

            _validator.CheckIfUserWasCreated(identityResult);
        }

        public async Task RemoveLoginAsync(ApplicationUser user, string provider, string providerKey)//TODO: validation
        {
            await _userManager.RemoveLoginAsync(user, provider, providerKey);
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        // OO sql generation...
        public async Task<bool> IsUserSoftDeletedAsync(string email)
        {
            var softDeleteUser = await _usersDbSet.FromSql($"SELECT * FROM [dbo].[AspNetUsers] WHERE Email = {email} AND IsDeleted = 1") // TODO: Change implementation
                .SingleOrDefaultAsync();

            return softDeleteUser != null;
        }

        //TODO: refactor
        public async Task<ApplicationUser> RestoreSoftDeletedUserByIdAsync(string id)
        {
            var user = await _usersDbSet.FromSql($"UPDATE [dbo].[AspNetUsers] SET[IsDeleted] = '0' WHERE Id = {id}") // TODO: Change implementation
                .SingleOrDefaultAsync();

            _validator.CheckIfUserExists(user);

            return user;
        }

        public async Task<ApplicationUser> RestoreSoftDeletedUserByEmailAsync(string email)
        {
            var user = await _usersDbSet.FromSql($"UPDATE [dbo].[AspNetUsers] SET[IsDeleted] = '0' WHERE Email = {email}") // TODO: Change implementation
                .SingleOrDefaultAsync();

            _validator.CheckIfUserExists(user);

            return user;
        }
    }
}
