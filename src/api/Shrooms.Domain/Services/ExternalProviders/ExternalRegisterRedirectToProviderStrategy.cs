using Microsoft.AspNetCore.WebUtilities;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Options;
using Shrooms.Domain.Helpers;
using Shrooms.Domain.Services.Users;
using Shrooms.Infrastructure.FireAndForget;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalRegisterRedirectToProviderStrategy : IExternalProviderStrategy
    {
        private readonly IApplicationSignInManager _signInManager;
        private readonly ITenantNameContainer _tenantNameContainer;

        private readonly ControllerRouteDto _routeDto;
        private readonly ExternalLoginRequestDto _requestDto;

        private readonly ApplicationOptions _applicationOptions;

        public ExternalRegisterRedirectToProviderStrategy(
            ITenantNameContainer tenantNameContainer,
            IApplicationSignInManager signInManager,
            ControllerRouteDto routeDto,
            ExternalLoginRequestDto requestDto,
            ApplicationOptions applicationOptions)
        {
            _tenantNameContainer = tenantNameContainer;
            _routeDto = routeDto;
            _signInManager = signInManager;
            _applicationOptions = applicationOptions;
            _requestDto = requestDto;
        }

        public Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var uri = ApplicationUrlHelper.GetActionUrl(_applicationOptions, _routeDto);

            var loginRedirectUri = _applicationOptions.GetClientLoginUrl(_tenantNameContainer.TenantName);
            
            var loginRedirectUrl = QueryHelpers.AddQueryString(
                loginRedirectUri,
                ExternalProviderConstants.AuthenticationTypeParameter,
                _requestDto.Provider);

            var redirectUrlParameters = new Dictionary<string, string>
            {
                { ExternalProviderConstants.OrganizationParameter, _tenantNameContainer.TenantName },
                { ExternalProviderConstants.ProviderParameter, _requestDto.Provider },
                { ExternalProviderConstants.ResponseTypeParameter, ExternalProviderConstants.ResponseType },
                { ExternalProviderConstants.RedirectUrlParameter, loginRedirectUrl },
                { ExternalProviderConstants.IsRegistrationParamaeter, ExternalProviderConstants.IsRegistration }
            };

            var redirectUrl = QueryHelpers.AddQueryString(uri, redirectUrlParameters);

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(_requestDto.Provider, redirectUrl);

            return Task.FromResult(new ExternalProviderResult(properties, _requestDto.Provider));
        }
    }
}
