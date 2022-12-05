using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Infrastructure;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.ExternalProviders.Strategies;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Users;
using Shrooms.Domain.ServiceValidators.Validators.ExternalProviders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalProviderStrategyFactory : IExternalProviderStrategyFactory
    {
        private readonly IApplicationSignInManager _signInManager;
        private readonly IOrganizationService _organizationService;
        private readonly IEnumerable<IExternalProviderStrategy> _strategies;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly IExternalProviderValidator _validator;

        public ExternalProviderStrategyFactory(
            IEnumerable<IExternalProviderStrategy> strategies,
            IApplicationSignInManager signInManager,
            ITenantNameContainer tenantNameContainer, 
            IExternalProviderValidator validator,
            IOrganizationService organizationService)
        {
            _signInManager = signInManager;
            _strategies = strategies;
            _tenantNameContainer = tenantNameContainer;
            _validator = validator;
            _organizationService = organizationService;
        }

        public IExternalProviderStrategy GetStrategy(ExternalProviderPartialResult partialResult)
        {
            var strategy = _strategies.Single(strategy => strategy.GetType() == partialResult.NextStrategy);
            strategy.SetArguments(partialResult.Arguments);
            return strategy;
        }

        public async Task<IExternalProviderStrategy> GetStrategyAsync(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto)
        {
            var organization = await GetOrganizationThatContainsProviderAsync(requestDto);
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            var strategy = _strategies.Single(strategy => strategy.CanBeUsed(externalLoginInfo, requestDto));
            strategy.SetArguments(organization, externalLoginInfo, routeDto, requestDto);
            return strategy;
        }

        private async Task<Organization> GetOrganizationThatContainsProviderAsync(ExternalLoginRequestDto requestDto)
        {
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);
            var hasProvider = _organizationService.HasProvider(organization, requestDto.Provider);
            _validator.CheckIfIsValidProvider(requestDto, hasProvider);
            return organization;
        }
    }
}
