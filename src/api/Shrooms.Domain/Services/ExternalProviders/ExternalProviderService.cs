using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Picture;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Domain.Services.Users;
using Shrooms.Infrastructure.FireAndForget;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shrooms.Domain.ServiceValidators.Validators.ExternalProviders;
using Shrooms.Domain.Services.AuthenticationStates;
using Shrooms.Domain.Services.ExternalProviders.Contexts;
using Shrooms.Domain.Services.ExternalProviders.Strategies;

namespace Shrooms.Domain.Services.ExternalProviders
{//Q: figure out where to redirect user (or what to do) when sign in is pressed but there are is no user
    public class ExternalProviderService : IExternalProviderService
    {
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;

        private readonly IExternalProviderContext _externalProviderContext;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly ApplicationOptions _applicationOptions;
        private readonly ITokenService _tokenService;
        private readonly IOrganizationService _organizationService;
        private readonly ICookieService _cookieService;
        private readonly IPictureService _pictureService;
        private readonly IExternalProviderValidator _validator;
        private readonly IAuthenticationStateService _stateService;

        private readonly AuthenticationService _authenticationService;

        public ExternalProviderService(
            ITokenService tokenService,
            IOptions<ApplicationOptions> applicationOptions,
            IExternalProviderContext externalProviderContext,
            IApplicationSignInManager signInManager,
            IApplicationUserManager userManager,
            ITenantNameContainer tenantNameContainer,
            IOrganizationService organizationService,
            ICookieService cookieService,
            IPictureService pictureService,
            IExternalProviderValidator validator,
            IAuthenticationService authenticationService,
            IAuthenticationStateService stateService)
        {
            _signInManager = signInManager;
            _externalProviderContext = externalProviderContext;
            _tenantNameContainer = tenantNameContainer;
            _tokenService = tokenService;
            _userManager = userManager;
            _organizationService = organizationService;
            _cookieService = cookieService;
            _pictureService = pictureService;
            _validator = validator;
            _stateService = stateService;

            _applicationOptions = applicationOptions.Value;
            _authenticationService = (AuthenticationService)authenticationService;
        }

        public async Task<ExternalProviderResult> ExternalLoginOrRegisterAsync(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto)
        {
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);
            var hasProvider = _organizationService.HasProvider(organization, requestDto.Provider);

            _validator.CheckIfValidProvider(requestDto, hasProvider);

            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            var strategy = GetStrategy(externalLoginInfo, requestDto, routeDto, organization);

            _externalProviderContext.SetStrategy(strategy);

            return await _externalProviderContext.ExecuteStrategyAsync();
        }

        public async Task<IEnumerable<ExternalLoginDto>> GetExternalLoginsAsync(ControllerRouteDto routeDto, string returnUrl, string userId)
        {
            var authenticationSchemes = await _authenticationService.Schemes.GetAllSchemesAsync();
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);

            return CreateExternalLogins(organization, authenticationSchemes, routeDto, returnUrl, userId);
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

        private IExternalProviderStrategy GetStrategy(
            ExternalLoginInfo externalLoginInfo,
            ExternalLoginRequestDto requestDto,
            ControllerRouteDto routeDto,
            Organization organization)
        {
            if (CanLinkAccount(externalLoginInfo, requestDto))
            {
                return new ExternalProviderLinkAccountStrategy(_userManager, requestDto, externalLoginInfo);
            }

            if (HasCookieFromExternalProvider(externalLoginInfo))
            {
                return requestDto.IsRegistration ?
                    new ExternalRegisterStrategy(
                        _tokenService,
                        _userManager,
                        _cookieService,
                        _pictureService,
                        requestDto,
                        externalLoginInfo,
                        organization) :
                    new ExternalLoginStrategy(_cookieService, _tokenService, externalLoginInfo);
            }

            return requestDto.IsRegistration ?
                new ExternalRegisterRedirectToProviderStrategy(
                    _tenantNameContainer,
                    _signInManager,
                    routeDto,
                    requestDto,
                    _applicationOptions) :
                new ExternalLoginRedirectToProviderStrategy(
                    _applicationOptions,
                    _tenantNameContainer,
                    _signInManager,
                    requestDto,
                    routeDto);
        }

        private string CreateExternalLoginUrl(
            ControllerRouteDto routeDto,
            AuthenticationScheme authenticationScheme,
            string returnUrl,
            string state,
            string userId,
            bool isRegistration)
        {
            var queryParams = new Dictionary<string, string>
            {
                { ExternalProviderConstants.ProviderParameter, authenticationScheme.Name },
                { ExternalProviderConstants.OrganizationParameter, _tenantNameContainer.TenantName },
                { ExternalProviderConstants.ResponseTypeParameter, ExternalProviderConstants.ResponseType },
                { ExternalProviderConstants.ClientIdParameter, _applicationOptions.ClientId },
                { ExternalProviderConstants.RedirectUrlParameter, new Uri($"{returnUrl}?{ExternalProviderConstants.AuthenticationTypeParameter}={authenticationScheme.Name}").AbsoluteUri },
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

        private static bool HasCookieFromExternalProvider(ExternalLoginInfo externalLoginInfo) => externalLoginInfo != null;

        private static bool CanLinkAccount(ExternalLoginInfo externalLoginInfo, ExternalLoginRequestDto requestDto) =>
            externalLoginInfo != null && requestDto.UserId != null;
    }
}
