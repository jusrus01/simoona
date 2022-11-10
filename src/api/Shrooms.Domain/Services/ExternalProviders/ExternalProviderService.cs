using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Infrastructure.FireAndForget;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalProviderService : IExternalProviderService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IExternalProviderContext _externalProviderContext;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly ApplicationOptions _applicationOptions;
        private readonly ITokenService _tokenService;
        private readonly IOrganizationService _organizationService;
        private readonly ICookieService _cookieService;

        public ExternalProviderService(
            ITokenService tokenService,
            IOptions<ApplicationOptions> applicationOptions,
            IExternalProviderContext externalProviderContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITenantNameContainer tenantNameContainer,
            IOrganizationService organizationService,
            ICookieService cookieService)
        {
            _signInManager = signInManager;
            _externalProviderContext = externalProviderContext;
            _tenantNameContainer = tenantNameContainer;
            _tokenService = tokenService;
            _userManager = userManager;
            _organizationService = organizationService;
            _cookieService = cookieService;

            _applicationOptions = applicationOptions.Value;
        }

        public async Task<ExternalProviderResult> ExternalLoginOrRegisterAsync(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto)
        {
            // TODO: Validate that ExternalLoginRequestDto has valid parameters

            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            var strategy = GetStrategy(externalLoginInfo, requestDto, routeDto);

            _externalProviderContext.SetStrategy(strategy);

            return await _externalProviderContext.ExecuteStrategyAsync();
        }

        private IExternalProviderStrategy GetStrategy(
            ExternalLoginInfo externalLoginInfo,
            ExternalLoginRequestDto requestDto,
            ControllerRouteDto routeDto)
        {
            if (externalLoginInfo != null && requestDto.UserId != null)
            {
                return new ExternalProviderLinkAccountStrategy();
            }

            if (externalLoginInfo != null)
            {
                return requestDto.IsRegistration ? 
                    new ExternalRegisterStrategy(
                        _tokenService,
                        externalLoginInfo,
                        _userManager,
                        _organizationService,
                        _tenantNameContainer,
                        _cookieService) :
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
    }
}
