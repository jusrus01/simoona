using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shrooms.Domain.Services.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalLoginStrategy : IExternalProviderStrategy
    {
        private readonly ITokenService _tokenService;
        private readonly ExternalLoginInfo _externalLoginInfo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExternalLoginStrategy(
            IHttpContextAccessor httpContextAccessor,
            ITokenService tokenService,
            ExternalLoginInfo externalLoginInfo)
        {
            _tokenService = tokenService;
            _externalLoginInfo = externalLoginInfo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var tokenRedirectUrl = await _tokenService.GetTokenRedirectUrlForExternalAsync(_externalLoginInfo);

            await SetExternalCookieAsync();
            
            return new ExternalProviderResult(tokenRedirectUrl);
        }

        private async Task SetExternalCookieAsync()
        {
            var claimsIdentity = new ClaimsIdentity(
                          new List<Claim>(),
                          CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
