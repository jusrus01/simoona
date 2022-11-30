using Microsoft.AspNetCore.WebUtilities;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Options;
using Shrooms.Domain.Helpers;
using Shrooms.Domain.Services.Users;
using Shrooms.Infrastructure.BackgroundJobs;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalLoginRedirectToProviderStrategy : IExternalProviderStrategy
    {
        private readonly IApplicationSignInManager _signInManager;
        private readonly ApplicationOptions _applicationOptions;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly ControllerRouteDto _routeDto;
        private readonly ExternalLoginRequestDto _requestDto;

        public ExternalLoginRedirectToProviderStrategy(
            ApplicationOptions applicationOptions,
            ITenantNameContainer tenantNameContainer,
            IApplicationSignInManager signInManager,
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
            var externalRegisterUri = ApplicationUrlHelper.GetActionUrl(_applicationOptions, _routeDto);
            var redirectUrl = QueryHelpers.AddQueryString(externalRegisterUri, ExternalProviderConstants.OrganizationParameter, _tenantNameContainer.TenantName);

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(_requestDto.Provider, redirectUrl);

            return Task.FromResult(new ExternalProviderResult(properties, _requestDto.Provider));
        }
    }
}
