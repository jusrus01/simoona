using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shrooms.Domain.ServiceValidators.Validators.ExternalProviders;
using Shrooms.Domain.Services.AuthenticationStates;
using Shrooms.Contracts.Infrastructure;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalProviderService : IExternalProviderService
    {
        private readonly IApplicationSignInManager _signInManager;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly ApplicationOptions _applicationOptions;
        private readonly IOrganizationService _organizationService;
        private readonly IExternalProviderValidator _validator;
        private readonly IAuthenticationStateService _stateService;
        private readonly IExternalProviderStrategyFactory _strategyFactory;

        private readonly AuthenticationService _authenticationService;

        public ExternalProviderService(
            IOptions<ApplicationOptions> applicationOptions,
            IApplicationSignInManager signInManager,
            ITenantNameContainer tenantNameContainer,
            IOrganizationService organizationService,
            IExternalProviderValidator validator,
            IAuthenticationService authenticationService,
            IAuthenticationStateService stateService,
            IExternalProviderStrategyFactory strategyFactory)
        {
            _signInManager = signInManager;
            _tenantNameContainer = tenantNameContainer;
            _organizationService = organizationService;
            _validator = validator;
            _stateService = stateService;
            _strategyFactory = strategyFactory;

            _applicationOptions = applicationOptions.Value;
            _authenticationService = (AuthenticationService)authenticationService;
        }

        public async Task<ExternalProviderResult> ExternalLoginOrRegisterAsync(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto)
        {
            var organization = await GetOrganizationThatContainsProviderAsync(requestDto);
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            var strategy = _strategyFactory.GetStrategy(externalLoginInfo, requestDto, routeDto, organization, out var parameters);
            return await strategy.ExecuteStrategyAsync(parameters, externalLoginInfo);
        }

        public async Task<IEnumerable<ExternalLoginDto>> GetExternalLoginsAsync(ControllerRouteDto routeDto, string returnUrl, string userId)
        {
            var authenticationSchemes = await _authenticationService.Schemes.GetAllSchemesAsync();
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);
            return CreateExternalLogins(organization, authenticationSchemes, routeDto, returnUrl, userId);
        }

        private async Task<Organization> GetOrganizationThatContainsProviderAsync(ExternalLoginRequestDto requestDto)
        {
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);
            var hasProvider = _organizationService.HasProvider(organization, requestDto.Provider);
            _validator.CheckIfIsValidProvider(requestDto, hasProvider);
            return organization;
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
