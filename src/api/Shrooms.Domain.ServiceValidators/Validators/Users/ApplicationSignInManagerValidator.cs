using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;

namespace Shrooms.Domain.ServiceValidators.Validators.Users
{
    public class ApplicationSignInManagerValidator : IApplicationSignInManagerValidator
    {
        public void CheckIfSuccessfulLogin(SignInResult signInResult) // Q: is it fine to depend on external types?
        {
            if (!signInResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.InvalidCredentials, "Invalid credentials");
            }
        }
    }
}
