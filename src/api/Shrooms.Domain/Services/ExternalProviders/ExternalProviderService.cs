using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DAL;
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
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{//Q: figure out where to redirect user (or what to do) when sign in is pressed but there are is no user
    public class ExternalProviderService : IExternalProviderService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationUserManager _userManager;

        private readonly IExternalProviderContext _externalProviderContext;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly ApplicationOptions _applicationOptions;
        private readonly ITokenService _tokenService;
        private readonly IOrganizationService _organizationService;
        private readonly ICookieService _cookieService;
        private readonly IPictureService _pictureService;
        private readonly IUnitOfWork2 _uow;

        public ExternalProviderService(
            ITokenService tokenService,
            IOptions<ApplicationOptions> applicationOptions,
            IExternalProviderContext externalProviderContext,
            SignInManager<ApplicationUser> signInManager,
            IApplicationUserManager userManager,
            ITenantNameContainer tenantNameContainer,
            IOrganizationService organizationService,
            ICookieService cookieService,
            IPictureService pictureService,
            IUnitOfWork2 uow)
        {
            _signInManager = signInManager;
            _externalProviderContext = externalProviderContext;
            _tenantNameContainer = tenantNameContainer;
            _tokenService = tokenService;
            _userManager = userManager;
            _organizationService = organizationService;
            _cookieService = cookieService;
            _pictureService = pictureService;
            _uow = uow;

            _applicationOptions = applicationOptions.Value;
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

        private static bool HasCookieFromExternalProvider(ExternalLoginInfo externalLoginInfo) => externalLoginInfo != null;

        private static bool CanLinkAccount(ExternalLoginInfo externalLoginInfo, ExternalLoginRequestDto requestDto) =>
            externalLoginInfo != null && requestDto.UserId != null;
    }
}
