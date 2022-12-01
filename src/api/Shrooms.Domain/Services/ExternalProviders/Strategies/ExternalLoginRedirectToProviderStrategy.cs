using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Options;
using Shrooms.Domain.Helpers;
using Shrooms.Domain.Services.Users;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalLoginRedirectToProviderStrategy : ExternalProviderStrategyBase
    {
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

        public override void CheckIfRequiredParametersAreSet()
        {
            EnsureParametersAreSet(Parameters.Route, Parameters.Request);
        }

        public override Task<ExternalProviderResult> ExecuteAsync()
        {
            var externalRegisterUri = ApplicationUrlHelper.GetActionUrl(_applicationOptions, Parameters.Route);
            var redirectUrl = QueryHelpers.AddQueryString(externalRegisterUri, ExternalProviderConstants.OrganizationParameter, _tenantNameContainer.TenantName);
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(Parameters.Request.Provider, redirectUrl);
            return Task.FromResult(new ExternalProviderResult(properties, Parameters.Request.Provider));
        }
    }
}
