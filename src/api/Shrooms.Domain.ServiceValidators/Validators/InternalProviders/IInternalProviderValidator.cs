using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Domain.ServiceValidators.Validators.InternalProviders
{
    public interface IInternalProviderValidator
    {
        void CheckIfDoesNotContainValidLogin(ApplicationUser user, bool hasExternalLogin);
    }
}
