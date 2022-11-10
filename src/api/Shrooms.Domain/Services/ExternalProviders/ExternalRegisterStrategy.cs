using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    /// <summary>
    /// Strategy that registers user
    /// </summary>
    public class ExternalRegisterStrategy : IExternalProviderStrategy
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ExternalLoginInfo _externalLoginInfo;
        private readonly ITokenService _tokenService;

        public ExternalRegisterStrategy(
            ITokenService tokenService,
            ExternalLoginInfo externalLoginInfo,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _externalLoginInfo = externalLoginInfo;
            _tokenService = tokenService;
        }

        public async Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var userEmail = _externalLoginInfo.Principal.FindFirst(ClaimTypes.Email).Value;

            if (userEmail == null) // TODO: Test it with Facebook provider
            {
                throw new ValidationException(ErrorCodes.Unspecified, "External provider did not provide email");
            }

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user != null)
            {
                throw new ValidationException(ErrorCodes.DuplicatesIntolerable, "User is registered"); // TODO: figure out if this should really throw
            }

            user = new ApplicationUser
            {
                Email = userEmail,
                UserName = userEmail,
                EmailConfirmed = true
            };

            var identityResult = await _userManager.CreateAsync(user, userEmail);
            
            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to create user");
            }

            identityResult = await _userManager.AddLoginAsync(user, _externalLoginInfo);

            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to add login");
            }

            var tokenRedirectUrl = await _tokenService.GetTokenRedirectUrlForExternalAsync(_externalLoginInfo);

            return new ExternalProviderResult(tokenRedirectUrl);
        }
    }
}
