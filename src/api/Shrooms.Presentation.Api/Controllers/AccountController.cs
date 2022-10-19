using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shrooms.Authentification.ExternalLoginInfrastructure;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Administration;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Permissions;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Infrastructure.FireAndForget;
using Shrooms.Presentation.Api.Controllers;
using Shrooms.Presentation.WebViewModels.Models;
using Shrooms.Presentation.WebViewModels.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// Checklist:
// Local token auth
// Login [x]
// Register []
//  w/o confirmation
//  w confirmation
namespace Shrooms.Presentation.Api
{
    [Authorize]
    [Route("Account")]
    public class AccountController : ShroomsControllerBase
    {
        private const int StateStrengthInBits = 256;
        
        private readonly ApplicationOptions _applicationOptions;
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IMapper _mapper;
    
        private readonly IOrganizationService _organizationService;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly IPermissionService _permissionService;
        private readonly AuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;
        private readonly IAdministrationUsersService _administrationUsersService;

        public AccountController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOrganizationService organizationService,
            ITenantNameContainer tenantNameContainer,
            IPermissionService permissionService,
            IAuthenticationService authenticationService,
            IAdministrationUsersService administrationUsersService,
            ITokenService tokenService,
            IOptions<ApplicationOptions> applicationOptions)
        {
            _applicationOptions = applicationOptions.Value;

            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _organizationService = organizationService;
            _tenantNameContainer = tenantNameContainer;
            _permissionService = permissionService;
            _authenticationService = (AuthenticationService)authenticationService;
            _tokenService = tokenService;
            _administrationUsersService = administrationUsersService;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registerDto = _mapper.Map<RegisterViewModel, RegisterDto>(registerViewModel);

                // TODO: Handle soft deleted user
                await _administrationUsersService.RegisterInternalAsync(registerDto);

                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequestWithError(e);
            }
        }

        [Authorize]
        [HttpGet("UserInfo")]
        public async Task<IActionResult> GetUserInfo() // TODO: Make sure that this is not used by external providers
        {
            try
            {
                var userInfoDto = await _administrationUsersService.GetUserInfoAsync(User.Identity);
                var userInfoViewModel = _mapper.Map<LoggedInUserInfoViewModel>(userInfoDto);

                return Ok(userInfoViewModel);
            }
            catch (ValidationException e)
            {
                return BadRequestWithError(e);
            }
        }

        [AllowAnonymous]
        [HttpGet("InternalLogins")]
        public async Task<IActionResult> GetInternalLogins()
        {
            var logins = new List<ExternalLoginViewModel>();
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);
            var organizationProviders = organization.AuthenticationProviders;

            if (!ContainsProvider(organizationProviders, AuthenticationConstants.InternalLoginProvider))
            {
                return Ok(logins);
            }

            var internalLogin = new ExternalLoginViewModel
            {
                Name = AuthenticationConstants.InternalLoginProvider
            };

            logins.Add(internalLogin);

            return Ok(logins);
        }


        [AllowAnonymous]
        [HttpGet("ExternalLogin")]
        public async Task<IActionResult> GetExternalLogin(string provider, string? client_Id = null, string? userId = null, bool isRegistration = false, string? error = null)
        {
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();

            if (externalLoginInfo != null)
            {
                return await CompleteExternalLoginAsync(externalLoginInfo);
            }

            var redirectUrl = Url.Action("ExternalLogin", "Account", new { organization = "SimoonaClone" });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        [HttpGet("ExternalLogins")]
        public async Task<ActionResult> GetExternalLogins(string returnUrl, bool isLinkable = false)
        {
            var externalLogins = new List<ExternalLoginViewModel>();

            var availableAuthenticationSchemes = await _authenticationService.Schemes.GetAllSchemesAsync();
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName); // TODO: Figure out ITenantNameContainer usage
            
            // TODO: Register scheme only if key exists
            foreach (var authenticationScheme in availableAuthenticationSchemes)
            {
                if (!ContainsProvider(organization.AuthenticationProviders, authenticationScheme.Name))
                {
                    continue;
                }

                var state = RandomOAuthStateGenerator.Generate(StateStrengthInBits);

                var login = new ExternalLoginViewModel
                {
                    Name = authenticationScheme.Name,
                    Url = CreateUrl(authenticationScheme, returnUrl, state, isLinkable, false),
                    State = state
                };

                externalLogins.Add(login);

                state = RandomOAuthStateGenerator.Generate(StateStrengthInBits);
                login = new ExternalLoginViewModel
                {
                    Name = $"{authenticationScheme.Name}Registration",
                    Url = CreateUrl(authenticationScheme, returnUrl, state, isLinkable, true),
                    State = state
                };

                externalLogins.Add(login);
            }

            return Ok(externalLogins);
        }

        // Responsible only for providing cookie, so we could access images... (not sure how google log in will work for that)
        [Authorize] // Make sure that token is received before this call?
        [HttpPost("SignIn")] // Idenity returns so .AspNetCore. cookie instead of .AspNet.Cookies
        public async Task<IActionResult> SignIn([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.UserName); // Username = email

            if (user == null)
            {
                return BadRequest();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("FullName", user.FullName),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignOutAsync();

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                //await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                return Ok();
            }
            return Forbid();

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            //var user = await _userManager.FindAsync(model.UserName, model.Password);

            //if (user == null)
            //{
            //    return BadRequest();
            //}

            //Authentication.SignOut(ShroomsDefaultAuthenticationTypes.ExternalCookie);

            //var oAuthIdentity = await _userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
            //var cookieIdentity = await _userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);

            //var properties = await CreateInitialRefreshToken(model.ClientId, user, oAuthIdentity);

            //SetCookieExpirationDateToAccessTokenLifeTime(properties);

            //Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);

            //await _userManager.AddLoginAsync(user.Id, new UserLoginInfo(AuthenticationConstants.InternalLoginProvider, user.Id));

            //return Ok();
        }

        private async Task<LoggedInUserInfoViewModel> GetLoggedInUserInfoAsync()
        {
            var userId = GetAuthenticatedUserId();
            var organizationId = GetOrganizationId();
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var user = await _userManager.FindByIdAsync(userId);
            var permissions = await _permissionService.GetUserPermissionsAsyncDeprecated(userId);

            var userInfo = new LoggedInUserInfoViewModel
            {
                HasRegistered = true,
                Roles = await _userManager.GetRolesAsync(user),
                UserName = User.Identity.Name,
                UserId = userId,
                OrganizationName = _tenantNameContainer.TenantName,
                OrganizationId = GetOrganizationId(),
                FullName = GetAuthenticatedUserFullName(),
                Permissions = permissions,
                Impersonated = claimsIdentity?.Claims.Any(c => c.Type == WebApiConstants.ClaimUserImpersonation && c.Value == true.ToString()) ?? false,
                CultureCode = user.CultureCode,
                TimeZone = user.TimeZone,
                PictureId = user.PictureId
            };

            return userInfo;
        }

        private static bool ContainsProvider(string providerList, string providerName)
        {
            return providerList.ToLower().Contains(providerName.ToLower());
        }

        private string CreateUrl(AuthenticationScheme authenticationScheme, string returnUrl, string state, bool isLinkable, bool isRegistration)
        {
            var controllerName = ControllerContext.ActionDescriptor.ControllerName;

            // TODO: Check if ExternalLogin exists? On start up?
            // TODO: Refactor

            return string.Concat(
                "/",
                controllerName,
                "/ExternalLogin?",
                $"provider={authenticationScheme.Name}&",
                $"organization={_tenantNameContainer.TenantName}&",
                $"response_type=token&",
                $"client_id={_applicationOptions.ClientId}&",
                $"redirect_url={new Uri($"{returnUrl}?authType={authenticationScheme.Name}").AbsoluteUri}&",
                $"state={state}",
                isLinkable ? $"userId={GetUserAndOrganization().UserId}" : "",
                isRegistration ? "isRegistration=true" : "");
        }

        private async Task<IActionResult> CompleteExternalLoginAsync(ExternalLoginInfo externalLoginInfo)
        {
            //var result = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey, false);

            //if (result.Succeeded)
            //{
            //    var accessToken = await HttpContext.GetTokenAsync(GoogleDefaults.AuthenticationScheme, "access_token");
            //    return Redirect($"http://localhost:3000/SimoonaClone/Login?authType=Google#access_token={accessToken}");
            //}

            var accessToken = await _tokenService.GetTokenForExternalAsync(externalLoginInfo);
            return Redirect($"http://localhost:3000/SimoonaClone/Login?authType=Google#access_token={accessToken}");
        }
    }
}