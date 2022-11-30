using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;

namespace Shrooms.Domain.ServiceValidators.Validators.Users
{
    public class ApplicationRoleManagerValidator : IApplicationRoleManagerValidator
    {
        public void CheckIfAddedToRole(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.RoleFailedToAdd, $"Failed to add user to role");
            }
        }

        public void CheckIfRemovedFromRole(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.RoleFailedToRemove, $"Failed to remove user from role");
            }
        }
    }
}
