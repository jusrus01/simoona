using Shrooms.Contracts.DataTransferObjects.Models.Users;

namespace Shrooms.Domain.ServiceValidators.Validators.ExternalProviders
{
    public interface IExternalProviderValidator
    {
        void CheckIfIsValidProvider(ExternalLoginRequestDto requestDto, bool hasProvider);
    }
}
