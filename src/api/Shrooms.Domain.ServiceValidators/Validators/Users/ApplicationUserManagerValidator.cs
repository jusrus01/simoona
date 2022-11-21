using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shrooms.Domain.ServiceValidators.Validators.Users
{
    public class ApplicationUserManagerValidator : IApplicationUserManagerValidator//TODO: Refactor
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserManagerValidator(UserManager<ApplicationUser> userManager)
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

        public void CheckIfPasswordResetWasSuccesfull(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to reset password");
            }
        }

        public void CheckIfUserExists(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ValidationException(ErrorCodes.UserNotFound, "User not found");
            }
        }

        public void CheckIfUserLoginsContainInternalLogin(IList<UserLoginInfo> logins)//Q: discuss conditional encapsulation
        {
            if (!logins.Any(login => login.LoginProvider == AuthenticationConstants.InternalLoginProvider))
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Could not find a valid provider for the account");
            }
        }

        public void CheckIfUserWasCreated(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to create user");//Q: Should these kind of methods be refactored with a common function that throws?
                // and if they do, should they be exported to specific validator e.g. IdentityResultValidator (shared usage...)?
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
