using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Domain.ServiceValidators.Validators.InternalProviders
{
    public class InternalProviderValidator : IInternalProviderValidator
    {
        public void CheckIfDoesNotContainValidLogin(ApplicationUser user, bool hasExternalLogin)
        {
            if (user.EmailConfirmed || hasExternalLogin)
            {
                throw new ValidationException(ErrorCodes.DuplicatesIntolerable, "User is already registered");
            }
        }
    }
}
