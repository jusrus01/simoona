using Shrooms.Contracts.DataTransferObjects.Models.Users;

namespace Shrooms.Domain.ServiceValidators.Validators.ExternalProviders
{
    public interface IExternalProviderValidator
    {
        void CheckIfValidProvider(ExternalLoginRequestDto requestDto, bool hasProvider);
    }
}
