using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Helpers;
using Shrooms.Infrastructure.FireAndForget;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    /// <summary>
    /// Strategy that redirects user to external provider to handle registration
    /// </summary>
    public class ExternalRegisterRedirectToProviderStrategy : IExternalProviderStrategy
    {
        private const string ResponseType = "token";
        private const string IsRegistration = "true";

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITenantNameContainer _tenantNameContainer;

        private readonly ControllerRouteDto _routeDto;
        private readonly ExternalLoginRequestDto _requestDto;

        private readonly ApplicationOptions _applicationOptions;

        public ExternalRegisterRedirectToProviderStrategy(
            ITenantNameContainer tenantNameContainer,
            SignInManager<ApplicationUser> signInManager,
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
            var uri = ShroomsUrlHelper.GetActionUrl(_applicationOptions, _routeDto);

            var loginRedirectUri = _applicationOptions.GetClientLoginUrl(_tenantNameContainer.TenantName);
            
            var loginRedirectUrl = QueryHelpers.AddQueryString(
                loginRedirectUri,
                ExternalProviderQueryParameterConstants.AuthenticationType,
                _requestDto.Provider);

            var redirectUrlParameters = new Dictionary<string, string>
            {
                { ExternalProviderQueryParameterConstants.Organization, _tenantNameContainer.TenantName },
                { ExternalProviderQueryParameterConstants.Provider, _requestDto.Provider },
                { ExternalProviderQueryParameterConstants.ResponseType, ResponseType },
                { ExternalProviderQueryParameterConstants.RedirectUrl, loginRedirectUrl },
                { ExternalProviderQueryParameterConstants.IsRegistration, IsRegistration }
            };

            var redirectUrl = QueryHelpers.AddQueryString(uri, redirectUrlParameters);

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(_requestDto.Provider, redirectUrl);

            return Task.FromResult(new ExternalProviderResult(properties, _requestDto.Provider));
        }
    }
}
