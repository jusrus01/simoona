using Shrooms.DataLayer.EntityModels.Models;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Users
{
    public interface IApplicationRoleManager
    {
        Task AddToRoleAsync(ApplicationUser user, string role);

        Task RemoveFromRoleAsync(ApplicationUser user, string role);

        Task<bool> IsInRoleAsync(ApplicationUser user, string role);

        Task<bool> IsInAnyOfTheRolesAsync(ApplicationUser user, params string[] roles);

        Task<bool> IsInAllRolesAsync(ApplicationUser user, params string[] roles);
    }
}
