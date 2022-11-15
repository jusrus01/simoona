using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using System.Threading.Tasks;

namespace Shrooms.Domain.ServiceValidators.Validators.Users
{
    public class UserManagerValidator : IUserManagerValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void CheckIfEmailWasConfirmed(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Invalid confirmation code");
            }
        }

        public void CheckIfLoginWasAdded(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to add external provider");
            }
        }

        public void CheckIfUserExists(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ValidationException(ErrorCodes.UserNotFound, "User not found");
            }
        }

        public void CheckIfUserWasCreated(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to create user");
            }
        }

        public async Task CheckPasswordAsync(ApplicationUser user, string password)
        {
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                throw new ValidationException(ErrorCodes.InvalidCredentials, "Invalid credentials");
            }
        }
    }
}
