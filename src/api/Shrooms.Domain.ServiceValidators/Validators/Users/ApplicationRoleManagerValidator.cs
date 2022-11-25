using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;

namespace Shrooms.Domain.ServiceValidators.Validators.Users
{
    public class ApplicationRoleManagerValidator : IApplicationRoleManagerValidator
    {
        public void CheckIfAddedToRole(IdentityResult identityResult, string role)
        {
            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.RoleManagerFailedToAdd, $"Failed to add user to {role} role");
            }
        }

        public void CheckIfRemovedFromRole(IdentityResult identityResult, string role)
        {
            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.RoleManagerFailedToRemove, $"Failed to remove user from {role} role");
            }
        }
    }
}
