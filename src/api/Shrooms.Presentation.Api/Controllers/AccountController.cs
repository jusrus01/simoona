//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web.Http;
//using AutoMapper;
//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.Cookies;
//using Microsoft.Owin.Security.Infrastructure;
//using Microsoft.Owin.Security.OAuth;
//using Shrooms.Presentation.Api.Helpers;
//using Shrooms.Presentation.Api.Providers;
//using Shrooms.Presentation.Api.Results;
//using Shrooms.Authentification.ExternalLoginInfrastructure;
//using Shrooms.Contracts.DataTransferObjects;
//using Shrooms.Contracts.Constants;
//using Shrooms.DataLayer.EntityModels.Models;
//using Shrooms.Domain.Services.Administration;
//using Shrooms.Domain.Services.Organizations;
//using Shrooms.Domain.Services.Permissions;
//using Shrooms.Domain.Services.RefreshTokens;
//using Shrooms.Presentation.WebViewModels.Models;
//using Shrooms.Presentation.WebViewModels.Models.AccountModels;
//using Shrooms.Contracts.Infrastructure;
//using Microsoft.AspNetCore.Http;
//using Shrooms.Authentification;

//namespace Shrooms.Presentation.Api.Controllers
//{
//    [Authorize]
//    [RoutePrefix("Account")]
//    public class AccountController : BaseController
//    {
//        private const int StateStrengthInBits = 256;
//        private readonly IMapper _mapper;
//        private readonly IPermissionService _permissionService;
//        private readonly IOrganizationService _organizationService;
//        private readonly IRefreshTokenService _refreshTokenService;
//        private readonly IAdministrationUsersService _administrationService;
//        private readonly IApplicationSettings _applicationSettings;

//        private IAuthenticationManager Authentication => Request.GetOwinContext().Authentication;

//        private string RequestedOrganization => Request.GetRequestedTenant();

//        public AccountController(
//            IMapper mapper, 
//            IHttpContextAccessor httpContextAccessor, 
//            IPermissionService permissionService,
//            IOrganizationService organizationService, 
//            IRefreshTokenService refreshTokenService, 
//            IAdministrationUsersService administrationService,
//            IApplicationSettings applicationSettings)
//        {
//            _mapper = mapper;
//            _permissionService = permissionService;
//            _organizationService = organizationService;
//            _refreshTokenService = refreshTokenService;
//            _administrationService = administrationService;
//            _applicationSettings = applicationSettings;
//        }

//        [HostAuthentication(ShroomsDefaultAuthenticationTypes.ExternalBearer)]
//        [Route("UserInfo")]
//        public async Task<IHttpActionResult> GetUserInfo()
//        {
//            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
//            if (externalLogin == null && User.Identity.IsAuthenticated)
//            {
//                var loggedUser = await GetLoggedInUserInfoAsync();
//                return Ok(loggedUser);
//            }
//            else
//            {
//                var externalUserInfo = new ExternalUserInfoViewModel
//                {
//                    Email = GetUserEmail(),
//                    HasRegistered = externalLogin == null,
//                    LoginProvider = externalLogin?.LoginProvider
//                };
//                return Ok(externalUserInfo);
//            }
//        }

//        [AllowAnonymous]
//        [Route("Register")]
//        public async Task<IHttpActionResult> RegisterUser([FromBody] RegisterViewModel model)
//        {
//            return Ok();
//            //if (!ModelState.IsValid)
//            //{
//            //    return BadRequest(ModelState);
//            //}

//            //if (await _administrationService.UserEmailExistsAsync(model.Email))
//            //{
//            //    var user = await _userManager.FindByEmailAsync(model.Email);

//            //    if (user == null || user.EmailConfirmed || !await _administrationService.HasExistingExternalLoginAsync(model.Email, AuthenticationConstants.InternalLoginProvider))
//            //    {
//            //        return BadRequest("User already exists");
//            //    }

//            //    await _userManager.RemovePasswordAsync(user.Id);
//            //    await _userManager.AddPasswordAsync(user.Id, model.Password);
//            //    await _administrationService.SendUserVerificationEmailAsync(user, RequestedOrganization);

//            //    return Ok();
//            //}

//            //if (await _administrationService.UserIsSoftDeletedAsync(model.Email))
//            //{
//            //    await _administrationService.RestoreUserAsync(model.Email);

//            //    return Ok();
//            //}

//            //var result = await _administrationService.CreateNewUserAsync(_mapper.Map<ApplicationUser>(model), model.Password, RequestedOrganization);

//            //if (!result.Succeeded)
//            //{
//            //    return GetErrorResult(result);
//            //}

//            //return Ok();
//        }

//        public async Task<IHttpActionResult> SignIn(LoginViewModel model)
//        {
//            return Ok();
//            //if (!ModelState.IsValid)
//            //{
//            //    return BadRequest();
//            //}

//            //var user = await _userManager.FindAsync(model.UserName, model.Password);

//            //if (user == null)
//            //{
//            //    return BadRequest();
//            //}

//            //Authentication.SignOut(ShroomsDefaultAuthenticationTypes.ExternalCookie);
//            //var oAuthIdentity = await _userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
//            //var cookieIdentity = await _userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);

//            //var properties = await CreateInitialRefreshToken(model.ClientId, user, oAuthIdentity);

//            //SetCookieExpirationDateToAccessTokenLifeTime(properties);

//            //Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);

//            //await _userManager.AddLoginAsync(user.Id, new UserLoginInfo(AuthenticationConstants.InternalLoginProvider, user.Id));

//            //return Ok();
//        }

//        [AllowAnonymous]
//        public async Task<IHttpActionResult> RequestPasswordReset(ForgotPasswordViewModel model)
//        {
//            return Ok();
//            //if (!ModelState.IsValid)
//            //{
//            //    return BadRequest();
//            //}

//            //var user = await _userManager.FindByEmailAsync(model.Email);

//            //if (user == null)
//            //{
//            //    return Ok();
//            //}

//            //await _administrationService.SendUserPasswordResetEmailAsync(user, RequestedOrganization);

//            //return Ok();
//        }

//        [AllowAnonymous]
//        public async Task<IHttpActionResult> VerifyEmail([FromBody] VerifyEmailViewModel model)
//        {
//            return Ok();
//            //if (!ModelState.IsValid)
//            //{
//            //    return BadRequest();
//            //}

//            //var user = await _userManager.FindByEmailAsync(model.Email);

//            //if (user == null)
//            //{
//            //    return BadRequest();
//            //}

//            //var result = await _userManager.ConfirmEmailAsync(user.Id, model.Code);

//            //if (!result.Succeeded)
//            //{
//            //    return GetErrorResult(result);
//            //}

//            //return Ok();
//        }

//        [AllowAnonymous]
//        public async Task<IHttpActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
//        {
//            return Ok();
//            //if (!ModelState.IsValid)
//            //{
//            //    return BadRequest();
//            //}

//            //var user = await _userManager.FindByEmailAsync(model.Email);

//            //if (user == null)
//            //{
//            //    return BadRequest();
//            //}

//            //var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);

//            //if (!result.Succeeded)
//            //{
//            //    return GetErrorResult(result);
//            //}

//            //return Ok();
//        }

//        [AllowAnonymous]
//        [Route("InternalLogins")]
//        public async Task<IHttpActionResult> GetInternalLogins()
//        {
//            var logins = new List<ExternalLoginViewModel>();
//            var organizationProviders = (await _organizationService.GetOrganizationByNameAsync(RequestedOrganization)).AuthenticationProviders;

//            if (!ContainsProvider(organizationProviders, AuthenticationConstants.InternalLoginProvider))
//            {
//                return Ok(logins);
//            }

//            var internalLogin = new ExternalLoginViewModel
//            {
//                Name = AuthenticationConstants.InternalLoginProvider
//            };

//            logins.Add(internalLogin);

//            return Ok(logins);
//        }

//        [Route("Logout")]
//        public async Task<IHttpActionResult> Logout()
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return Ok();
//            }

//            var userAndOrganization = GetUserAndOrganization();
//            await _refreshTokenService.RemoveTokenBySubjectAsync(userAndOrganization);
//            _permissionService.RemoveCache(userAndOrganization.UserId);
//            Authentication.SignOut();

//            return Ok();
//        }

//        [AllowAnonymous]
//        [Route("ExternalLogins")]
//        public async Task<IHttpActionResult> GetExternalLogins(string returnUrl, bool isLinkable = false)
//        {
//            return Ok();
//            //var descriptions = Authentication.GetExternalAuthenticationTypes();
//            //var logins = new List<ExternalLoginViewModel>();

//            //var organizationProviders = (await _organizationService.GetOrganizationByNameAsync(RequestedOrganization)).AuthenticationProviders;

//            //foreach (var description in descriptions)
//            //{
//            //    if (!ContainsProvider(organizationProviders, description.Caption))
//            //    {
//            //        continue;
//            //    }
//            //    var state = RandomOAuthStateGenerator.Generate(StateStrengthInBits);

//            //    var login = new ExternalLoginViewModel
//            //    {
//            //        Name = description.Caption,
//            //        Url = CreateUrl(description, returnUrl, state, isLinkable, false),
//            //        State = state
//            //    };

//            //    logins.Add(login);

//            //    state = RandomOAuthStateGenerator.Generate(StateStrengthInBits);
//            //    login = new ExternalLoginViewModel
//            //    {
//            //        Name = $"{description.Caption}Registration",
//            //        Url = CreateUrl(description, returnUrl, state, isLinkable, true),
//            //        State = state
//            //    };

//            //    logins.Add(login);
//            //}

//            //return Ok(logins);
//        }

//        [OverrideAuthentication]
//        [HostAuthentication(ShroomsDefaultAuthenticationTypes.ExternalBearer)]
//        [Route("RegisterExternal")]
//        public async Task<IHttpActionResult> RegisterExternal()
//        {
//            return Ok();
//            //if (!ModelState.IsValid)
//            //{
//            //    return BadRequest(ModelState);
//            //}

//            //var info = await Authentication.GetExternalLoginInfoAsync();
//            //if (info == null)
//            //{
//            //    return InternalServerError();
//            //}

//            //if (!await _administrationService.UserEmailExistsAsync(info.Email))
//            //{
//            //    if (await _administrationService.UserIsSoftDeletedAsync(info.Email))
//            //    {
//            //        await _administrationService.RestoreUserAsync(info.Email);
//            //    }
//            //    else
//            //    {
//            //        var requestedOrganization = RequestedOrganization;
//            //        var result = await _administrationService.CreateNewUserWithExternalLoginAsync(info, requestedOrganization);
//            //        if (!result.Succeeded)
//            //        {
//            //            return GetErrorResult(result);
//            //        }
//            //    }
//            //}
//            //else if (await _administrationService.HasExistingExternalLoginAsync(info.Email, info.Login.LoginProvider))
//            //{
//            //    var user = await _userManager.FindByEmailAsync(info.Email);
//            //    await _administrationService.AddProviderImageAsync(user.Id, info.ExternalIdentity);
//            //    return Ok("User already exists");
//            //}
//            //else if (await _administrationService.HasExistingExternalLoginAsync(info.Email, AuthenticationConstants.InternalLoginProvider))
//            //{
//            //    var user = await _userManager.FindByEmailAsync(info.Email);
//            //    if (user?.EmailConfirmed == false)
//            //    {
//            //        await _userManager.RemoveLoginAsync(user.Id, new UserLoginInfo(AuthenticationConstants.InternalLoginProvider, user.Id));
//            //        await _userManager.RemovePasswordAsync(user.Id);
//            //    }
//            //}

//            //var userId = (await _userManager.FindByEmailAsync(info.Email)).Id;
//            //await _userManager.AddLoginAsync(userId, info.Login);
//            //await _administrationService.AddProviderImageAsync(userId, info.ExternalIdentity);
//            //await _administrationService.AddProviderEmailAsync(userId, info.Login.LoginProvider, info.Email);

//            //return Ok();
//        }

//        [OverrideAuthentication]
//        [HostAuthentication(ShroomsDefaultAuthenticationTypes.ExternalCookie)]
//        [AllowAnonymous]
//        [Route("ExternalLogin", Name = "ExternalLogin")]
//        // ReSharper disable once InconsistentNaming
//        public async Task<IHttpActionResult> GetExternalLogin(string provider, string client_Id = null, string userId = null, bool isRegistration = false, string error = null)
//        {
//            return Ok();
//            //if (string.IsNullOrEmpty(client_Id) || error != null)
//            //{
//            //    var uri = CreateErrorUri("error");
//            //    return Redirect(uri);
//            //}

//            //if (!User.Identity.IsAuthenticated)
//            //{
//            //    return new ChallengeResult(provider, this);
//            //}

//            //var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

//            //if (externalLogin.Email == null)
//            //{
//            //    var uri = CreateErrorUri("emailError");
//            //    Authentication.SignOut(ShroomsDefaultAuthenticationTypes.ExternalCookie);
//            //    return Redirect(uri);
//            //}

//            //if (externalLogin.LoginProvider != provider)
//            //{
//            //    Authentication.SignOut(ShroomsDefaultAuthenticationTypes.ExternalCookie);
//            //    return new ChallengeResult(provider, this);
//            //}

//            //var user = await _userManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));
//            //var hasLogin = user != null;

//            //if (isRegistration && hasLogin == false)
//            //{
//            //    var isEmailHostValid = await _organizationService.IsOrganizationHostValidAsync(externalLogin.Email, RequestedOrganization);
//            //    if (!isEmailHostValid)
//            //    {
//            //        var uri = CreateErrorUri("error");
//            //        Authentication.SignOut(ShroomsDefaultAuthenticationTypes.ExternalCookie);
//            //        return Redirect(uri);
//            //    }
//            //}

//            //// Linking accounts.
//            //if (userId != null)
//            //{
//            //    return await LinkAccountsAsync(externalLogin, userId);
//            //}

//            //// Registration process.
//            //if (isRegistration == true)
//            //{
//            //    return await RegisterOrLoginAsync(user, externalLogin, client_Id, hasLogin);
//            //}

//            //// Login process.
//            //return await LoginAsync(user, externalLogin, client_Id, hasLogin);
//        }

//        private static bool ContainsProvider(string providerList, string providerName)
//        {
//            return providerList.ToLower().Contains(providerName.ToLower());
//        }

//        private async Task<AuthenticationProperties> CreateInitialRefreshToken(string clientId, ApplicationUser user, ClaimsIdentity oAuthIdentity)
//        {
//            var userOrganization = new UserAndOrganizationDto
//            {
//                OrganizationId = user.OrganizationId,
//                UserId = user.Id
//            };

//            await _refreshTokenService.RemoveTokenBySubjectAsync(userOrganization);

//            var properties = ApplicationOAuthProvider.CreateProperties(user.Id, clientId);

//            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
//            var context = new AuthenticationTokenCreateContext(Request.GetOwinContext(), Startup.OAuthServerOptions.RefreshTokenFormat, ticket);

//            await Startup.OAuthServerOptions.RefreshTokenProvider.CreateAsync(context);
//            properties.Dictionary.Add("refresh_token", context.Token);
//            return properties;
//        }

//        private async Task<LoggedInUserInfoViewModel> GetLoggedInUserInfoAsync()
//        {
//            throw new NotImplementedException();
//        //    var userId = GetUserId();
//        //    var organizationId = User.Identity.GetOrganizationId();
//        //    var claimsIdentity = User.Identity as ClaimsIdentity;

//        //    var user = await _userManager.FindByIdAsync(userId);
//        //    var permissions = await _permissionService.GetUserPermissionsAsync(userId, organizationId);

//        //    var userInfo = new LoggedInUserInfoViewModel
//        //    {
//        //        HasRegistered = true,
//        //        Roles = await _userManager.GetRolesAsync(userId),
//        //        UserName = User.Identity.Name,
//        //        UserId = userId,
//        //        OrganizationName = GetOrganizationName(),
//        //        OrganizationId = GetOrganizationId(),
//        //        FullName = GetUserFullName(),
//        //        Permissions = permissions,
//        //        Impersonated = claimsIdentity?.Claims.Any(c => c.Type == WebApiConstants.ClaimUserImpersonation && c.Value == true.ToString()) ?? false,
//        //        CultureCode = user.CultureCode,
//        //        TimeZone = user.TimeZone,
//        //        PictureId = user.PictureId
//        //    };

//        //    return userInfo;
//        //}

//        //private IHttpActionResult GetErrorResult(IdentityResult result)
//        //{
//        //    if (result == null)
//        //    {
//        //        return InternalServerError();
//        //    }

//        //    if (result.Succeeded)
//        //    {
//        //        return null;
//        //    }

//        //    if (result.Errors != null)
//        //    {
//        //        foreach (var error in result.Errors)
//        //        {
//        //            ModelState.AddModelError("", error);
//        //        }
//        //    }

//        //    if (ModelState.IsValid)
//        //    {
//        //        // No ModelState errors are available to send, so just return an empty BadRequest.
//        //        return BadRequest();
//        //    }

//        //    return BadRequest(ModelState);
//        }

//        private string CreateUrl(AuthenticationDescription description, string returnUrl, string state, bool isLinkable, bool isRegistration)
//        {
//            var url = Url.RouteFromController("ExternalLogin",
//                        ControllerContext.ControllerDescriptor.ControllerName,
//                        new
//                        {
//                            provider = description.AuthenticationType,
//                            organization = RequestedOrganization,
//                            response_type = "token",
//                            client_id = Startup.JsAppClientId,
//                            redirect_uri = new Uri(Request.RequestUri, $"{returnUrl}?authType={description.AuthenticationType}").AbsoluteUri,
//                            state = state,
//                            userId = isLinkable ? GetUserAndOrganization().UserId : null,
//                            isRegistration = isRegistration ? "true" : null
//                        });
//            return url;
//        }

//        private async Task<IHttpActionResult> LinkAccountsAsync(ExternalLoginData externalLogin, string userId)
//        {
//            return Ok();
//            //var info = await Authentication.GetExternalLoginInfoAsync();
//            //if (await _userManager.AddLoginAsync(userId, info.Login) == null)
//            //{
//            //    var uri = CreateErrorUri("error");
//            //    return Redirect(uri);
//            //}

//            //Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
//            //var identity = new ClaimsIdentity(externalLogin.GetClaims(), OAuthDefaults.AuthenticationType);
//            //Authentication.SignIn(identity);
//            //await _administrationService.AddProviderEmailAsync(userId, info.Login.LoginProvider, info.Email);

//            //return Ok();
//        }

//        private async Task<IHttpActionResult> RegisterOrLoginAsync(ApplicationUser user, ExternalLoginData externalLogin, string clientId, bool hasLogin)
//        {
//            return Ok();
//            //if (hasLogin)
//            //{
//            //    await UpdateCookiesAndLoginAsync(user, externalLogin, clientId);
//            //}
//            //else if (await _administrationService.UserEmailExistsAsync(externalLogin.Email))
//            //{
//            //    if (await _administrationService.HasExistingExternalLoginAsync(externalLogin.Email, externalLogin.LoginProvider))
//            //    {
//            //        var uri = CreateErrorUri("providerExists");
//            //        Authentication.SignOut(ShroomsDefaultAuthenticationTypes.ExternalCookie);
//            //        return Redirect(uri);
//            //    }

//            //    var userId = (await _userManager.FindByEmailAsync(externalLogin.Email)).Id;
//            //    var info = await Authentication.GetExternalLoginInfoAsync();

//            //    if (await _userManager.AddLoginAsync(userId, info.Login) == null)
//            //    {
//            //        var uri = CreateErrorUri("error");
//            //        Authentication.SignOut(ShroomsDefaultAuthenticationTypes.ExternalCookie);
//            //        return Redirect(uri);
//            //    }

//            //    var identity = new ClaimsIdentity(externalLogin.GetClaims(), OAuthDefaults.AuthenticationType);
//            //    Authentication.SignIn(identity);
//            //}
//            //else
//            //{
//            //    var identity = new ClaimsIdentity(externalLogin.GetClaims(), OAuthDefaults.AuthenticationType);
//            //    Authentication.SignIn(identity);
//            //}

//            //return Ok();
//        }

//        private async Task<IHttpActionResult> LoginAsync(ApplicationUser user, ExternalLoginData externalLogin, string clientId, bool hasLogin)
//        {
//            if (hasLogin)
//            {
//                await UpdateCookiesAndLoginAsync(user, externalLogin, clientId);
//            }
//            else
//            {
//                if (await _administrationService.UserEmailExistsAsync(externalLogin.Email) == false)
//                {
//                    var uri = CreateErrorUri("notFound");
//                    Authentication.SignOut(ShroomsDefaultAuthenticationTypes.ExternalCookie);
//                    return Redirect(uri);
//                }

//                if (await _administrationService.HasExistingExternalLoginAsync(externalLogin.Email, externalLogin.LoginProvider))
//                {
//                    var uri = CreateErrorUri("providerExists");
//                    Authentication.SignOut(ShroomsDefaultAuthenticationTypes.ExternalCookie);
//                    return Redirect(uri);
//                }

//                var identity = new ClaimsIdentity(externalLogin.GetClaims(), OAuthDefaults.AuthenticationType);
//                Authentication.SignIn(identity);
//            }

//            return Ok();
//        }

//        private string CreateErrorUri(string tag)
//        {
//            var hostUri = Request.GetQueryNameValuePairs().First(e => e.Key == "redirect_uri").Value;
//            var encodedError = Uri.EscapeDataString("Access_denied");
//            return $"{hostUri}#{tag}={encodedError}";
//        }

//        private async Task UpdateCookiesAndLoginAsync(ApplicationUser user, ExternalLoginData externalLogin, string clientId)
//        {
//            throw new NotImplementedException();
//            //Authentication.SignOut(ShroomsDefaultAuthenticationTypes.ExternalCookie);
//            //var oAuthIdentity = await _userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
//            //var cookieIdentity = await _userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);
//            //var properties = await CreateInitialRefreshToken(clientId, user, oAuthIdentity);

//            //if ((externalLogin.LoginProvider == "Google" && user.GoogleEmail == null) || (externalLogin.LoginProvider == "Facebook" && user.FacebookEmail == null))
//            //{
//            //    await _administrationService.AddProviderEmailAsync(user.Id, externalLogin.LoginProvider, externalLogin.Email);
//            //}

//            //Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
//        }

//        private void SetCookieExpirationDateToAccessTokenLifeTime(AuthenticationProperties properties)
//        {
//            // Set the .AspNet.Cookies. expiration date to the same date as the access token lifetime
//            var lifeTimeHoursTimeSpan = TimeSpan.FromHours(Convert.ToInt16(_applicationSettings.AccessTokenLifeTimeInHours));
//            var expirationDate = DateTime.UtcNow.Add(lifeTimeHoursTimeSpan);

//            properties.ExpiresUtc = DateTime.SpecifyKind(expirationDate, DateTimeKind.Utc);
//        }
//    }
//}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shrooms.Authentification.ExternalLoginInfrastructure;
using Shrooms.Contracts.Constants;
using Shrooms.DataLayer.EntityModels.Models;
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

namespace Shrooms.Presentation.Api
{
    [Route("Account")]
    public class AccountController : ShroomsControllerBase
    {
        private const int StateStrengthInBits = 256;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOrganizationService _organizationService;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly IPermissionService _permissionService;
        private readonly AuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOrganizationService organizationService,
            ITenantNameContainer tenantNameContainer,
            IPermissionService permissionService,
            IAuthenticationService authenticationService,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _organizationService = organizationService;
            _tenantNameContainer = tenantNameContainer;
            _permissionService = permissionService;
            _authenticationService = (AuthenticationService)authenticationService;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet("UserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null && User.Identity.IsAuthenticated)
            {
                var loggedUser = await GetLoggedInUserInfoAsync();
                return Ok(loggedUser);
            }
            else
            {
                var externalUserInfo = new ExternalUserInfoViewModel
                {
                    Email = GetAuthenticatedUserEmail(),
                    HasRegistered = externalLogin == null,
                    LoginProvider = externalLogin?.LoginProvider
                };
                return Ok(externalUserInfo);
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

        [HttpGet("Google/Success")]
        public async Task<IActionResult> OnGoogleSignInSuccess()
        {
            //    ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            //    if (info == null)
            //        return RedirectToAction(nameof(Login));

            //    var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            //    string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            //    if (result.Succeeded)
            //        return View(userInfo);
            //    else
            //    {
            //        AppUser user = new AppUser
            //        {
            //            Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
            //            UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
            //        };

            //        IdentityResult identResult = await userManager.CreateAsync(user);
            //        if (identResult.Succeeded)
            //        {
            //            identResult = await userManager.AddLoginAsync(user, info);
            //            if (identResult.Succeeded)
            //            {
            //                await signInManager.SignInAsync(user, false);
            //                return View(userInfo);
            //            }
            //        }
            //        return AccessDenied();

            return Ok();
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
            var permissions = await _permissionService.GetUserPermissionsAsync(userId);

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
                $"client_id={_configuration["ClientId"]}&",
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