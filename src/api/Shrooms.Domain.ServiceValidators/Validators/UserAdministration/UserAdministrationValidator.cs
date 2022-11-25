using System;
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
    }
}
