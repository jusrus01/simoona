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

        public override void CheckIfRequiredParametersAreSet()
        {
            EnsureParametersAreSet(Parameters.LoginInfo);
        }

        public override async Task<ExternalProviderResult> ExecuteAsync()
        {
            var tokenRedirectUrl = await _tokenService.GetTokenRedirectUrlForExternalAsync(Parameters.LoginInfo);
            await _cookieService.SetExternalCookieAsync();
            return new ExternalProviderResult(tokenRedirectUrl);
        }
    }
}
