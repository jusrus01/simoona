using Microsoft.AspNetCore.Identity;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.ServiceValidators.Validators.Users;
using System.Linq;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Users
{
    public class ApplicationRoleManager : IApplicationRoleManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationRoleManagerValidator _validator;

        public ApplicationRoleManager(UserManager<ApplicationUser> userManager, IApplicationRoleManagerValidator validator)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public async Task AddToRoleAsync(ApplicationUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            _validator.CheckIfAddedToRole(result, role);
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, role);
            _validator.CheckIfRemovedFromRole(result, role);
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> IsInAnyOfTheRolesAsync(ApplicationUser user, params string[] roles)
        {
            foreach (var role in roles)
            {
                if (await _userManager.IsInRoleAsync(user, role))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> IsInAllRolesAsync(ApplicationUser user, params string[] roles)
        {
            foreach (var role in roles)
            {
                if (!await _userManager.IsInRoleAsync(user, role))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
