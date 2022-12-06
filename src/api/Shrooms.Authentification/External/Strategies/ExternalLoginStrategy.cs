using Microsoft.AspNetCore.Identity;
using Shrooms.Authentication.External.Arguments;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Tokens;
using System.Threading.Tasks;

namespace Shrooms.Authentication.External.Strategies
{
    public class ExternalLoginStrategy : ExternalProviderStrategyBase, IExternalProviderStrategy<LoginArgs>
    {
        private LoginArgs _arguments;

        private readonly ITokenService _tokenService;
        private readonly ICookieService _cookieService;

        public ExternalLoginStrategy(ICookieService cookieService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _cookieService = cookieService;
        }

        public bool CanBeUsed(ExternalLoginInfo loginInfo, ExternalLoginRequestDto requestDto) =>
            loginInfo != null &&
            !requestDto.IsRegistration &&
            requestDto.UserId == null;

        public async Task<ExternalProviderPartialResult> ExecuteAsync()
        {
            var tokenRedirectUrl = await _tokenService.GetTokenRedirectUrlForExternalAsync(_arguments.LoginInfo);
            await _cookieService.SetExternalCookieAsync();
            return Complete(new ExternalProviderResult(tokenRedirectUrl));
        }

        public void SetArguments(params object[] arguments) =>
            SetArguments(MapArgumentsToRequiredArgument<LoginArgs>(
                arguments,
                typeof(ExternalLoginInfo),
                typeof(ExternalLoginRequestDto)));

        public void SetArguments(LoginArgs arguments) =>
            _arguments = arguments;
    }
}
