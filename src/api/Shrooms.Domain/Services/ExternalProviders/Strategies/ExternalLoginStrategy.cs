using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.ExternalProviders;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Tokens;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalLoginStrategy : ExternalProviderStrategyBase
    {
        private readonly ITokenService _tokenService;
        private readonly ICookieService _cookieService;

        public ExternalLoginStrategy(ICookieService cookieService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _cookieService = cookieService;
        }

        public override void EnsureValidParameters(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null)
        {
            EnsureParametersAreSet(loginInfo);
        }

        public override async Task<ExternalProviderResult> ExecuteAsync(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null)
        {
            var tokenRedirectUrl = await _tokenService.GetTokenRedirectUrlForExternalAsync(loginInfo);
            await _cookieService.SetExternalCookieAsync();
            return new ExternalProviderResult(tokenRedirectUrl);
        }
    }
}
