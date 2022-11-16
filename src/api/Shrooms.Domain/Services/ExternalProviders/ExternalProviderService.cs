using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Picture;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Domain.Services.Users;
using Shrooms.Infrastructure.FireAndForget;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{//Q: figure out where to redirect user (or what to do) when sign in is pressed but there are is no user
    public class ExternalProviderService : IExternalProviderService
    {
        private const int StateStrengthInBits = 256;

        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;

        private readonly IExternalProviderContext _externalProviderContext;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly ApplicationOptions _applicationOptions;
        private readonly ITokenService _tokenService;
        private readonly IOrganizationService _organizationService;
        private readonly ICookieService _cookieService;
        private readonly IPictureService _pictureService;

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
            IAuthenticationService authenticationService)
        {
            _signInManager = signInManager;
            _externalProviderContext = externalProviderContext;
            _tenantNameContainer = tenantNameContainer;
            _tokenService = tokenService;
            _userManager = userManager;
            _organizationService = organizationService;
            _cookieService = cookieService;
            _pictureService = pictureService;

            _applicationOptions = applicationOptions.Value;
            _authenticationService = (AuthenticationService)authenticationService;
        }

        public async Task<ExternalProviderResult> ExternalLoginOrRegisterAsync(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto)
        {
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);

            if (!_organizationService.HasProvider(organization, requestDto.Provider))
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Invalid provider");
            }

            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            var strategy = GetStrategy(externalLoginInfo, requestDto, routeDto, organization);

            _externalProviderContext.SetStrategy(strategy);

            return await _externalProviderContext.ExecuteStrategyAsync();
        }

        // TODO: Register scheme only if key exists (on start up, before adding facebook signin)
        public async Task<IEnumerable<ExternalLoginDto>> GetExternalLoginsAsync(string controllerName, string returnUrl, string userId)
        {
            var externalLogins = new List<ExternalLoginDto>();

            var availableAuthenticationSchemes = await _authenticationService.Schemes.GetAllSchemesAsync();
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);

            foreach (var authenticationScheme in availableAuthenticationSchemes)
            {
                if (!_organizationService.HasProvider(organization, authenticationScheme.Name))
                {
                    continue;
                }

                externalLogins.AddRange(new List<ExternalLoginDto>
                {
                    CreateExternalLogin(controllerName, authenticationScheme, returnUrl, userId, isRegistration: false),
                    CreateExternalLogin(controllerName, authenticationScheme, returnUrl, userId, isRegistration: true),
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

        // TODO: Refactor
        private string CreateExternalLoginUrl(
            string controllerName,
            AuthenticationScheme authenticationScheme,
            string returnUrl,
            string state,
            string userId,
            bool isRegistration)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "provider", authenticationScheme.Name },
                { "organization", _tenantNameContainer.TenantName },
                { "response_type", "token" },
                { "client_id", _applicationOptions.ClientId },
                { "redirect_url", new Uri($"{returnUrl}?authType={authenticationScheme.Name}").AbsoluteUri },
                { "state", state }
            };

            if (userId != null)
            {
                queryParams["userId"] = userId;
            }

            if (isRegistration)
            {
                queryParams["isRegistration"] = "true";
            }

            return QueryHelpers.AddQueryString($"/{controllerName}/ExternalLogin", queryParams);
        }

        private ExternalLoginDto CreateExternalLogin(
            string controllerName,
            AuthenticationScheme authenticationScheme,
            string returnUrl,
            string userId,
            bool isRegistration)
        {
            var state = GenerateExternalAuthenticationState();

            return new ExternalLoginDto
            {
                Name = !isRegistration ? authenticationScheme.Name : $"{authenticationScheme.Name}Registration",
                Url = CreateExternalLoginUrl(controllerName, authenticationScheme, returnUrl, state, userId, isRegistration),
                State = state
            };
        }

        // TODO: Refactor
        private string GenerateExternalAuthenticationState()
        {
            const int bitsPerByte = 8;

            using var cryptoProvider = new RNGCryptoServiceProvider();

            if (StateStrengthInBits % bitsPerByte != 0)
            {
                throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
            }

            var strengthInBytes = StateStrengthInBits / bitsPerByte;

            var data = new byte[strengthInBytes];

            cryptoProvider.GetBytes(data);

            var base64 = Convert.ToBase64String(data);

            return base64[0..^1];
        }

        private static bool HasCookieFromExternalProvider(ExternalLoginInfo externalLoginInfo) => externalLoginInfo != null;

        private static bool CanLinkAccount(ExternalLoginInfo externalLoginInfo, ExternalLoginRequestDto requestDto) =>
            externalLoginInfo != null && requestDto.UserId != null;
    }
}
