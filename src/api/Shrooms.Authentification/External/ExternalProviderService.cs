using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Organizations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shrooms.Domain.Services.AuthenticationStates;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Authentication.External.Strategies;

namespace Shrooms.Authentication.External
{
    public class ExternalProviderService : IExternalProviderService
    {
        private readonly ApplicationOptions _applicationOptions;

        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly IOrganizationService _organizationService;
        private readonly IAuthenticationStateService _stateService;
        private readonly IExternalProviderStrategyFactory _strategyFactory;

        private readonly AuthenticationService _authenticationService;

        public ExternalProviderService(
            IOptions<ApplicationOptions> applicationOptions,
            ITenantNameContainer tenantNameContainer,
            IOrganizationService organizationService,
            IAuthenticationService authenticationService,
            IAuthenticationStateService stateService,
            IExternalProviderStrategyFactory strategyFactory)
        {
            _applicationOptions = applicationOptions.Value;

            _tenantNameContainer = tenantNameContainer;
            _organizationService = organizationService;
            _stateService = stateService;
            _strategyFactory = strategyFactory;

            _authenticationService = (AuthenticationService)authenticationService;
        }

        public async Task<ExternalProviderResult> ExternalLoginOrRegisterAsync(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto)
        {
            var strategy = await _strategyFactory.GetStrategyAsync(requestDto, routeDto);
            return await ResolveStrategyAsync(strategy);
        }

        public async Task<IEnumerable<ExternalLoginDto>> GetExternalLoginsAsync(ControllerRouteDto routeDto, string returnUrl, string userId)
        {
            var authenticationSchemes = await _authenticationService.Schemes.GetAllSchemesAsync();
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);
            return CreateExternalLogins(organization, authenticationSchemes, routeDto, returnUrl, userId);
        }

        private async Task<ExternalProviderResult> ResolveStrategyAsync(IExternalProviderStrategy strategy)
        {
            var partialResult = await strategy.ExecuteAsync();

            while (!partialResult.IsComplete)
            {
                var nextStrategy = _strategyFactory.GetStrategy(partialResult);
                partialResult = await nextStrategy.ExecuteAsync();
            }

            return partialResult.Result;
        }

        private IList<ExternalLoginDto> CreateExternalLogins(
            Organization organization,
            IEnumerable<AuthenticationScheme> authenticationSchemes,
            ControllerRouteDto routeDto,
            string returnUrl,
            string userId)
        {
            var externalLogins = new List<ExternalLoginDto>();

            foreach (var authenticationScheme in authenticationSchemes)
            {
                if (!_organizationService.HasProvider(organization, authenticationScheme.Name))
                {
                    continue;
                }

                externalLogins.AddRange(new List<ExternalLoginDto>
                {
                    CreateExternalLogin(routeDto, authenticationScheme, returnUrl, userId, isRegistration: false),
                    CreateExternalLogin(routeDto, authenticationScheme, returnUrl, userId, isRegistration: true),
                });
            }

            return externalLogins;
        }

        private ExternalLoginDto CreateExternalLogin(
            ControllerRouteDto routeDto,
            AuthenticationScheme authenticationScheme,
            string returnUrl,
            string userId,
            bool isRegistration,
            string registrationPrefix = "Registration")
        {
            var state = _stateService.GenerateExternalAuthenticationState();

            return new ExternalLoginDto
            {
                Name = !isRegistration ? authenticationScheme.Name : $"{authenticationScheme.Name}{registrationPrefix}",
                Url = CreateExternalLoginUrl(routeDto, authenticationScheme, returnUrl, state, userId, isRegistration),
                State = state
            };
        }

        private string CreateExternalLoginUrl(
            ControllerRouteDto routeDto,
            AuthenticationScheme authenticationScheme,
            string returnUrl,
            string state,
            string userId,
            bool isRegistration)
        {
            var redirectUri = new Uri($"{returnUrl}?{ExternalProviderConstants.AuthenticationTypeParameter}={authenticationScheme.Name}").AbsoluteUri;
            var queryParams = new Dictionary<string, string>
            {
                { ExternalProviderConstants.ProviderParameter, authenticationScheme.Name },
                { ExternalProviderConstants.OrganizationParameter, _tenantNameContainer.TenantName },
                { ExternalProviderConstants.ResponseTypeParameter, ExternalProviderConstants.ResponseType },
                { ExternalProviderConstants.ClientIdParameter, _applicationOptions.ClientId },
                { ExternalProviderConstants.RedirectUrlParameter, redirectUri },
                { ExternalProviderConstants.StateParameter, state }
            };

            if (userId != null)
            {
                queryParams[ExternalProviderConstants.UserIdParameter] = userId;
            }

            if (isRegistration)
            {
                queryParams[ExternalProviderConstants.IsRegistrationParameter] = ExternalProviderConstants.IsRegistration;
            }

            return QueryHelpers.AddQueryString($"/{routeDto.ControllerName}/{routeDto.ActionName}", queryParams);
        }
    }
}
