using Microsoft.AspNetCore.Identity;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Tokens;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalLoginStrategy : IExternalProviderStrategy
    {
        private readonly ITokenService _tokenService;
        private readonly ExternalLoginInfo _externalLoginInfo;
        private readonly ICookieService _cookieService;

        public ExternalLoginStrategy(
            ICookieService cookieService,
            ITokenService tokenService,
            ExternalLoginInfo externalLoginInfo)
        {
            _tokenService = tokenService;
            _externalLoginInfo = externalLoginInfo;
            _cookieService = cookieService;
        }

        public async Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var tokenRedirectUrl = await _tokenService.GetTokenRedirectUrlForExternalAsync(_externalLoginInfo);

            await _cookieService.SetExternalCookieAsync();
            
            return new ExternalProviderResult(tokenRedirectUrl);
        }
    }
}
