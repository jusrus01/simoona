using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Options;
using Shrooms.Domain.Helpers;
using Shrooms.Domain.Services.ExternalProviders.Arguments;
using Shrooms.Domain.Services.Users;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalLoginRedirectToProviderStrategy : ExternalProviderStrategyBase, IExternalProviderStrategy<LoginRedirectArgs>
    {
        private LoginRedirectArgs _arguments;

        private readonly IApplicationSignInManager _signInManager;
        private readonly ITenantNameContainer _tenantNameContainer;
        
        private readonly ApplicationOptions _applicationOptions;

        public ExternalLoginRedirectToProviderStrategy(
            IOptions<ApplicationOptions> applicationOptions,
            ITenantNameContainer tenantNameContainer,
            IApplicationSignInManager signInManager)
        {
            _signInManager = signInManager;
            _applicationOptions = applicationOptions.Value;
            _tenantNameContainer = tenantNameContainer;
        }

        public bool CanBeUsed(ExternalLoginInfo loginInfo, ExternalLoginRequestDto requestDto) =>
            loginInfo == null &&
            requestDto.UserId == null &&
            !requestDto.IsRegistration;
        
        public Task<ExternalProviderPartialResult> ExecuteAsync()
        {
            var properties = CreateProperties();
            var completeResult = Complete(new ExternalProviderResult(properties, _arguments.Request.Provider));
            return Task.FromResult(completeResult);
        }

        public void SetArguments(LoginRedirectArgs arguments) => _arguments = arguments;

        public void SetArguments(params object[] arguments) =>
            SetArguments(MapArgumentsToRequiredArgument<LoginRedirectArgs>(
                arguments,
                typeof(ExternalLoginRequestDto),
                typeof(ControllerRouteDto)));

        private AuthenticationProperties CreateProperties()
        {
            var externalRegisterUri = ApplicationUrlHelper.GetActionUrl(_applicationOptions, _arguments.Route);
            var redirectUrl = QueryHelpers.AddQueryString(externalRegisterUri, ExternalProviderConstants.OrganizationParameter, _tenantNameContainer.TenantName);
            return _signInManager.ConfigureExternalAuthenticationProperties(_arguments.Request.Provider, redirectUrl);
        }
    }
}