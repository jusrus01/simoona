using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.ServiceValidators.Validators.Users;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Users
{
    public class ApplicationSignInManager : IApplicationSignInManager
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationSignInManagerValidator _validator;

        public ApplicationSignInManager(SignInManager<ApplicationUser> signInManager, IApplicationSignInManagerValidator validator)
        {
            _signInManager = signInManager;
            _validator = validator;
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task ExternalLoginSignInAsync(ExternalLoginInfo externalLoginInfo)
        {
            var result = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey, false);
            _validator.CheckIfSuccessfulLogin(result);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }
    }
}
