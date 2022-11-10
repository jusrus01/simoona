using Microsoft.AspNetCore.Identity;
using Shrooms.Domain.Services.Tokens;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalLoginStrategy : IExternalProviderStrategy
    {
        private readonly ITokenService _tokenService;
        private readonly ExternalLoginInfo _externalLoginInfo;

        public ExternalLoginStrategy(ITokenService tokenService, ExternalLoginInfo externalLoginInfo)
        {
            _tokenService = tokenService;
            _externalLoginInfo = externalLoginInfo;
        }

        public async Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var tokenRedirectUrl = await _tokenService.GetTokenRedirectUrlForExternalAsync(_externalLoginInfo);

            return new ExternalProviderResult(tokenRedirectUrl);
        }
    }
}
