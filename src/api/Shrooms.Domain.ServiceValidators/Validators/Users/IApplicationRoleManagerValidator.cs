using Microsoft.AspNetCore.Identity;

namespace Shrooms.Domain.ServiceValidators.Validators.Users
{
    public interface IApplicationRoleManagerValidator
    {
        void CheckIfAddedToRole(IdentityResult identityResult);

        void CheckIfRemovedFromRole(IdentityResult identityResult);
    }
}
