using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
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
            IOptions<ApplicationOptions> applicationOptions,
            ITenantNameContainer tenantNameContainer,
            IApplicationSignInManager signInManager)
        {
            _tenantNameContainer = tenantNameContainer;
            _signInManager = signInManager;
            _applicationOptions = applicationOptions.Value;
        }

        public override void CheckIfRequiredParametersAreSet()
        {
            EnsureParametersAreSet(Parameters.Route, Parameters.Request);
        }

        public override Task<ExternalProviderResult> ExecuteAsync()
        {
            var uri = ApplicationUrlHelper.GetActionUrl(_applicationOptions, Parameters.Route);

            var loginRedirectUri = _applicationOptions.GetClientLoginUrl(_tenantNameContainer.TenantName);
            
            var loginRedirectUrl = QueryHelpers.AddQueryString(
                loginRedirectUri,
                ExternalProviderConstants.AuthenticationTypeParameter,
                Parameters.Request.Provider);

            var redirectUrlParameters = new Dictionary<string, string>
            {
                { ExternalProviderConstants.OrganizationParameter, _tenantNameContainer.TenantName },
                { ExternalProviderConstants.ProviderParameter, Parameters.Request.Provider },
                { ExternalProviderConstants.ResponseTypeParameter, ExternalProviderConstants.ResponseType },
                { ExternalProviderConstants.RedirectUrlParameter, loginRedirectUrl },
                { ExternalProviderConstants.IsRegistrationParameter, ExternalProviderConstants.IsRegistration }
            };

            var redirectUrl = QueryHelpers.AddQueryString(uri, redirectUrlParameters);

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(Parameters.Request.Provider, redirectUrl);

            return Task.FromResult(new ExternalProviderResult(properties, Parameters.Request.Provider));
        }
    }
}
