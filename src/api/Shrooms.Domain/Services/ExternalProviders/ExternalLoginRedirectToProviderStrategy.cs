using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Helpers;
using Shrooms.Infrastructure.FireAndForget;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    /// <summary>
    /// Strategy that redirects user to external provider to handle login
    /// </summary>
    public class ExternalLoginRedirectToProviderStrategy : IExternalProviderStrategy
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationOptions _applicationOptions;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly ControllerRouteDto _routeDto;
        private readonly ExternalLoginRequestDto _requestDto;

        public ExternalLoginRedirectToProviderStrategy(
            ApplicationOptions applicationOptions,
            ITenantNameContainer tenantNameContainer,
            SignInManager<ApplicationUser> signInManager,
            ExternalLoginRequestDto requestDto,
            ControllerRouteDto routeDto)
        {
            _signInManager = signInManager;
            _applicationOptions = applicationOptions;
            _tenantNameContainer = tenantNameContainer;
            _routeDto = routeDto;
            _requestDto = requestDto;
        }

        public Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var externalRegisterUri = ShroomsUrlHelper.GetActionUrl(_applicationOptions, _routeDto);
            var redirectUrl = QueryHelpers.AddQueryString(externalRegisterUri, ExternalProviderQueryParameterConstants.Organization, _tenantNameContainer.TenantName);

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(_requestDto.Provider, redirectUrl);

            return Task.FromResult(new ExternalProviderResult(properties, _requestDto.Provider));
        }
    }
}
