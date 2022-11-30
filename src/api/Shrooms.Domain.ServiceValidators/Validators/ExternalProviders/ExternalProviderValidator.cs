using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;

namespace Shrooms.Domain.ServiceValidators.Validators.ExternalProviders
{
    public class ExternalProviderValidator : IExternalProviderValidator
    {
        public void CheckIfIsValidProvider(ExternalLoginRequestDto requestDto, bool hasProvider)
        {
            if (!string.IsNullOrEmpty(requestDto.Provider) && !hasProvider)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Invalid provider");
            }
        }
    }
}
