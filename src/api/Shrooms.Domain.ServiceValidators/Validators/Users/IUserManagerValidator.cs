using Microsoft.AspNetCore.Identity;
using Shrooms.DataLayer.EntityModels.Models;
using System.Threading.Tasks;

namespace Shrooms.Domain.ServiceValidators.Validators.Users
{
    public interface IUserManagerValidator
    {
        void CheckIfUserExists(ApplicationUser user);

        void CheckIfLoginWasAdded(IdentityResult result);

        void CheckIfEmailWasConfirmed(IdentityResult result);

        void CheckIfUserWasCreated(IdentityResult result);

        Task CheckPasswordAsync(ApplicationUser user, string password);
    }
}
