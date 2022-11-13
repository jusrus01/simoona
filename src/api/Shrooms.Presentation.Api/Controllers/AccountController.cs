using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.Domain.Services.Administration;
using Shrooms.Domain.Services.ExternalProviders;
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
    public class AccountController : ShroomsControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IAdministrationUsersService _administrationUsersService;
        private readonly IExternalProviderService _externalProviderService;

        public AccountController(
            IMapper mapper,
            IAdministrationUsersService administrationUsersService,
            IExternalProviderService externalProviderService)
        {
            _mapper = mapper;
            _administrationUsersService = administrationUsersService;
            _externalProviderService = externalProviderService;
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
            var externalLoginDtos = await _administrationUsersService.GetExternalLoginsAsync(
                ControllerContext.ActionDescriptor.ControllerName,
                returnUrl,
                isLinkable ? GetUserAndOrganization().UserId : null);

            var externalLoginViewModels = _mapper.Map<IEnumerable<ExternalLoginViewModel>>(externalLoginDtos);

            return Ok(externalLoginViewModels);
        }

        /// <summary>
        /// Only responsible for providing a cookie that is used in StorageController
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
    }
}