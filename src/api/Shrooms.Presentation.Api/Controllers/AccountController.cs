using AutoMapper;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Administration;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Infrastructure.FireAndForget;
using Shrooms.Presentation.Api.Controllers;
using Shrooms.Presentation.WebViewModels.Models;
using Shrooms.Presentation.WebViewModels.Models.AccountModels;
using System.Collections.Generic;
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
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IMapper _mapper;
    
        private readonly ITokenService _tokenService;
        private readonly IAdministrationUsersService _administrationUsersService;
        private readonly ITenantNameContainer _tenantNameContainer;

        public AccountController(
            IMapper mapper,
            SignInManager<ApplicationUser> signInManager,
            IAdministrationUsersService administrationUsersService,
            ITokenService tokenService,
            ITenantNameContainer tenantNameContainer)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _administrationUsersService = administrationUsersService;
            _tenantNameContainer = tenantNameContainer;
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
            try
            {
                var loginDtos = await _administrationUsersService.GetInternalLoginsAsync();
                var loginViewModels = _mapper.Map<IEnumerable<ExternalLoginViewModel>>(loginDtos);

                return Ok(loginViewModels);
            }
            catch (ValidationException e)
            {
                return BadRequestWithError(e);
            }
        }

        [AllowAnonymous]
        [HttpGet("ExternalLogin")]
        // TODO: Figure out what to do with these params (and if they are necessary)
        // TODO: Return .AspNet.Cookies. cookie here, since we get it from older API here
        public async Task<IActionResult> ExternalLogin(
            string provider,
            string? client_Id = null,
            string? userId = null,
            bool isRegistration = false,
            string? error = null)
        {
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();

            if (externalLoginInfo != null)
            {
                return await CompleteExternalLoginAsync(externalLoginInfo);
            }

            var redirectUrl = Url.Action(
                ControllerContext.ActionDescriptor.ActionName,
                ControllerContext.ActionDescriptor.ControllerName,
                new { organization = _tenantNameContainer.TenantName });
            
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl);

            return new ChallengeResult(GoogleDefaults.AuthenticationScheme, properties);
        }

        [AllowAnonymous]
        [HttpGet("ExternalLogins")]
        public async Task<ActionResult> GetExternalLogins(string returnUrl, bool isLinkable = false)
        {
            var externalLoginDtos = await _administrationUsersService.GetExternalLoginsAsync(
                ControllerContext.ActionDescriptor.ControllerName,
                returnUrl,
                isLinkable ? GetUserAndOrganization().UserId : null);

            var externalLoginViewModels = _mapper.Map<IEnumerable<ExternalLoginViewModel>>(externalLoginDtos);

            return Ok(externalLoginViewModels);
        }

        // TODO: Ask about refresh token
        // TODO: Make sure that we receive this .AspNet.Cookies when using external providers
        /// <summary>
        /// Only responsible for providing a cookie for StorageController
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SetSignInCookie([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var loginDto = _mapper.Map<LoginDto>(loginViewModel);

                await _administrationUsersService.SetSignInCookieAsync(loginDto);

                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequestWithError(e);
            }
        }

        private async Task<IActionResult> CompleteExternalLoginAsync(ExternalLoginInfo externalLoginInfo)
        {
            try
            {
                return Redirect(await _tokenService.GetTokenRedirectUrlForExternalAsync(externalLoginInfo));
            }
            catch (ValidationException e)
            {
                return BadRequestWithError(e);
            }
        }
    }
}