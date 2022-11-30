using System;
using System.Security.Principal;

namespace Shrooms.Domain.ServiceValidators.Validators.UserAdministration
{
    public interface IUserAdministrationValidator
    {
        void CheckIfEmploymentDateIsSet(DateTime? employmentDate);

        void CheckIfUserHasFirstLoginRole(bool hasRole);

        void CheckIfIsValidClaimsIdentity(IIdentity identity);
    }
}