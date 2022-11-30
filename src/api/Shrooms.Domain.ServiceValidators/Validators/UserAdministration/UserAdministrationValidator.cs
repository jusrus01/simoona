using System;
using System.Security.Claims;
using System.Security.Principal;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;

namespace Shrooms.Domain.ServiceValidators.Validators.UserAdministration
{
    public class UserAdministrationValidator : IUserAdministrationValidator
    {
        public void CheckIfEmploymentDateIsSet(DateTime? employmentDate)
        {
            if (!employmentDate.HasValue)
            {
                throw new ValidationException(ErrorCodes.UserAdministrationInvalidEmploymentDate, "Employment date is not valid");
            }
        }

        public void CheckIfUserHasFirstLoginRole(bool hasRole)
        {
            if (hasRole)
            {
                throw new ValidationException(ErrorCodes.UserAdministrationInformationNotProvided, "User has not filled info yet");
            }
        }

        public void CheckIfIsValidClaimsIdentity(IIdentity identity)
        {
            if (identity is ClaimsIdentity)
            {
                CheckIfClaimsIndentityIsAuthenticated(identity as ClaimsIdentity);
                return;
            }

            throw new ValidationException(ErrorCodes.UserAdministrationInvalidIdentity, "Invalid identity");
        }

        private void CheckIfClaimsIndentityIsAuthenticated(ClaimsIdentity claimsIdentity)
        {
            if (!claimsIdentity.IsAuthenticated)
            {
                throw new ValidationException(ErrorCodes.UserAdministrationIdentityNotAuthenticated, "User is not authenticated");
            }
        }
    }
}
