using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;

namespace Shrooms.Domain.ServiceValidators.Validators.Users
{
    public class ApplicationSignInManagerValidator : IApplicationSignInManagerValidator
    {
        public void CheckIfSuccessfulLogin(SignInResult signInResult)
        {
            if (!signInResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.InvalidCredentials, "Invalid credentials");
            }
        }
    }
}
