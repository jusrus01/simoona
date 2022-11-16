using Microsoft.AspNetCore.Identity;

namespace Shrooms.Domain.ServiceValidators.Validators.Users
{
    public interface IApplicationSignInManagerValidator
    {
        void CheckIfSuccessfulLogin(SignInResult signInResult);
    }
}
