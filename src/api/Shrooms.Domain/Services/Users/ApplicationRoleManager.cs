using Microsoft.AspNetCore.Identity;
using Shrooms.DataLayer.EntityModels.Models;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Users
{
    public class ApplicationRoleManager : IApplicationRoleManager
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationRoleManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddToRoleAsync(ApplicationUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            throw new System.NotImplementedException();
        }
    }
}
