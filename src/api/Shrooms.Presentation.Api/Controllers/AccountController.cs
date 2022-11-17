using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.Domain.Services.Administration;
using Shrooms.Domain.Services.ExternalProviders;
using Shrooms.Domain.Services.InternalProviders;
using Shrooms.Presentation.Api.Controllers;
using Shrooms.Presentation.Api.ShroomsExtensions;
using Shrooms.Presentation.WebViewModels.Models;
using Shrooms.Presentation.WebViewModels.Models.AccountModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api
{
    [Authorize]
    [Route("Account")]
    public class AccountController : ApplicationControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IAdministrationUsersService _administrationUsersService;
        private readonly IExternalProviderService _externalProviderService;
        private readonly IInternalProviderService _internalProviderService;

        public AccountController(
            IMapper mapper,
            IAdministrationUsersService administrationUsersService,
            IExternalProviderService externalProviderService,
            IInternalProviderService internalProviderService)
        {
            _mapper = mapper;

            _administrationUsersService = administrationUsersService;
            _externalProviderService = externalProviderService;
            _internalProviderService = internalProviderService;
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

                await _internalProviderService.RegisterAsync(registerDto);

                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequestWithError(e);
            }
        }

        [Authorize]
        [HttpGet("UserInfo")]
        public async Task<IActionResult> GetUserInfo()
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
                var loginDtos = await _internalProviderService.GetLoginsAsync();
                var loginViewModels = _mapper.Map<IEnumerable<ExternalLoginViewModel>>(loginDtos);

                return Ok(loginViewModels);
            }
            catch (ValidationException e)
            {
                return BadRequestWithError(e);
            }
        }

        // Q: why do we redirect to client instead of api?
        [AllowAnonymous]
        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailViewModel verifyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var verifyDto = _mapper.Map<VerifyEmailDto>(verifyViewModel);

                await _administrationUsersService.VerifyEmailAsync(verifyDto);

                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequestWithError(ex);
            }
        }

        /// <summary>
        /// This method handles external provider registration and login. 
        /// Based on the sent request parameters, it either registers or signs in the user.
        /// </summary>
        /// <param name="requestViewModel">Parameters</param>
        /// <returns>Redirects to an external provider or redirects to the client with an access token and set .AspNet.Cookies cookie</returns>
        [AllowAnonymous]
        [HttpGet("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin(ExternalLoginRequestViewModel requestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var requestDto = _mapper.Map<ExternalLoginRequestDto>(requestViewModel);
                
                var routeDto = new ControllerRouteDto
                {
                    ControllerName = ControllerContext.ActionDescriptor.ControllerName,
                    ActionName = ControllerContext.ActionDescriptor.ActionName
                };

                var externalProviderResult = await _externalProviderService.ExternalLoginOrRegisterAsync(requestDto, routeDto);

                return externalProviderResult.ToActionResult(this);
            }
            catch (ValidationException ex)
            {
                return BadRequestWithError(ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("ExternalLogins")]
        public async Task<ActionResult> GetExternalLogins(string returnUrl, bool isLinkable = false)
        {
            var externalLoginDtos = await _externalProviderService.GetExternalLoginsAsync(
                ControllerContext.ActionDescriptor.ControllerName,
                returnUrl,
                GetUserId(isLinkable));

            var externalLoginViewModels = _mapper.Map<IEnumerable<ExternalLoginViewModel>>(externalLoginDtos);

            return Ok(externalLoginViewModels);
        }

        private string? GetUserId(bool isLinkable) => isLinkable ? GetUserAndOrganization().UserId : null;

        /// <summary>
        /// Only responsible for providing a cookie that is used in StorageController
        /// </summary>
        /// <param name="loginViewModel">Deprecated parameter</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SignIn")]
#pragma warning disable IDE0060 // Remove unused parameter
        public async Task<IActionResult> SetSignInCookie([FromBody] LoginViewModel loginViewModel)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _internalProviderService.SetSignInCookieAsync();

                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequestWithError(e);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> RequestPasswordReset(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _administrationUsersService.SendUserPasswordResetEmailAsync(model.Email);
             
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequestWithError(ex);
            }
        }
    }
}