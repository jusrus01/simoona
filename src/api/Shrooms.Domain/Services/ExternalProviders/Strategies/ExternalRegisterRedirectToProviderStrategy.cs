using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Shrooms.Contracts.DataTransferObjects.Models.ExternalProviders;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Options;
using Shrooms.Domain.Helpers;
using Shrooms.Domain.Services.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalRegisterRedirectToProviderStrategy : ExternalProviderStrategyBase
    {
        private readonly IApplicationSignInManager _signInManager;
        private readonly ITenantNameContainer _tenantNameContainer;

        private readonly ApplicationOptions _applicationOptions;

        public ExternalRegisterRedirectToProviderStrategy(
            ITenantNameContainer tenantNameContainer,
            IApplicationSignInManager signInManager,
            ApplicationOptions applicationOptions)
        {
            _tenantNameContainer = tenantNameContainer;
            _signInManager = signInManager;
            _applicationOptions = applicationOptions;
        }

        public override void EnsureValidParameters(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null)
        {
            EnsureParametersAreSet(parameters.Route, parameters.Request);
        }

        public override Task<ExternalProviderResult> ExecuteAsync(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null)
        {
            var uri = ApplicationUrlHelper.GetActionUrl(_applicationOptions, parameters.Route);

            var loginRedirectUri = _applicationOptions.GetClientLoginUrl(_tenantNameContainer.TenantName);
            
            var loginRedirectUrl = QueryHelpers.AddQueryString(
                loginRedirectUri,
                ExternalProviderConstants.AuthenticationTypeParameter,
                parameters.Request.Provider);

            var redirectUrlParameters = new Dictionary<string, string>
            {
                { ExternalProviderConstants.OrganizationParameter, _tenantNameContainer.TenantName },
                { ExternalProviderConstants.ProviderParameter, parameters.Request.Provider },
                { ExternalProviderConstants.ResponseTypeParameter, ExternalProviderConstants.ResponseType },
                { ExternalProviderConstants.RedirectUrlParameter, loginRedirectUrl },
                { ExternalProviderConstants.IsRegistrationParameter, ExternalProviderConstants.IsRegistration }
            };

            var redirectUrl = QueryHelpers.AddQueryString(uri, redirectUrlParameters);

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(parameters.Request.Provider, redirectUrl);

            return Task.FromResult(new ExternalProviderResult(properties, parameters.Request.Provider));
        }
    }
}
