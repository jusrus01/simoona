using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Administration;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Picture;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Infrastructure.FireAndForget;
using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    /// <summary>
    /// Strategy that registers user
    /// </summary>
    public class ExternalRegisterStrategy : IExternalProviderStrategy
    {//Q: get feedback on this and on ExternalResult
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ICookieService _cookieService;
        private readonly ExternalLoginInfo _externalLoginInfo;
        private readonly ITokenService _tokenService;
        private readonly IPictureService _pictureService;
        private readonly IAdministrationUsersService _userAdministrationService;
        private readonly IUnitOfWork2 _uow;
        private readonly ExternalLoginRequestDto _requestDto;
        private readonly Organization _organization;

        public ExternalRegisterStrategy(
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            ICookieService cookieService,
            IPictureService pictureService,
            IAdministrationUsersService userAdministrationService,
            IUnitOfWork2 uow,
            ExternalLoginRequestDto requestDto,
            ExternalLoginInfo externalLoginInfo,
            Organization organization)
        {
            _requestDto = requestDto;

            _userManager = userManager;
            _externalLoginInfo = externalLoginInfo;
            _tokenService = tokenService;
            _cookieService = cookieService;
            _pictureService = pictureService;
            _userAdministrationService = userAdministrationService;
            _uow = uow;
            _organization = organization;
        }

        // TODO: Refactor
        public async Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var claimsIdentity = _externalLoginInfo.Principal.Identity as ClaimsIdentity;
            
            var userEmail = claimsIdentity.FindFirst(ClaimTypes.Email).Value;

            if (userEmail == null) // TODO: Test it with Facebook provider
            {
                throw new ValidationException(ErrorCodes.Unspecified, "External provider did not provide email");
            }
            
            if (await _userAdministrationService.IsUserSoftDeletedAsync(userEmail))
            {
                return await new ExternalProviderLinkAccountStrategy(
                    _userManager,
                    _uow,
                    _requestDto,
                    _externalLoginInfo,
                    restoreUser: true)
                    .ExecuteStrategyAsync();
            }

            var registeredUser = await _userManager.FindByEmailAsync(userEmail);

            if (registeredUser != null && registeredUser.EmailConfirmed)
            {
                return await ExecuteExternalLoginStrategyAsync();
            }

            var unconfirmedInternalAccountFound = registeredUser != null && !registeredUser.EmailConfirmed;

            // TODO: Fill in more, retrieve from claims and validate from another provider
            // TODO: Make sure that the email will be filled in by another service call
            var newUser = await CreateNewUserAsync(claimsIdentity, userEmail);

            if (unconfirmedInternalAccountFound)
            {
                await _userManager.RemoveLoginAsync(registeredUser, AuthenticationConstants.InternalLoginProvider, registeredUser.Id);
                await _userManager.RemovePasswordAsync(registeredUser);
                
                registeredUser.FirstName = newUser.FirstName;
                registeredUser.LastName = newUser.LastName;
                registeredUser.Email = newUser.Email;
                registeredUser.UserName = newUser.UserName;
                registeredUser.EmailConfirmed = newUser.EmailConfirmed;
                registeredUser.OrganizationId = newUser.OrganizationId;
                registeredUser.PictureId = newUser.PictureId;
            }

            var identityResult = unconfirmedInternalAccountFound ?
                await _userManager.UpdateAsync(registeredUser) :
                await _userManager.CreateAsync(newUser);

            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to create user");
            }

            var user = unconfirmedInternalAccountFound ? registeredUser : newUser;

            identityResult = await _userManager.AddLoginAsync(user, _externalLoginInfo);

            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to add login");
            }

            return await ExecuteExternalLoginStrategyAsync();
        }

        private async Task<ApplicationUser> CreateNewUserAsync(ClaimsIdentity claimsIdentity, string email)
        {
            var userFirstName = claimsIdentity.FindFirst(ClaimTypes.GivenName).Value;
            var userLastName = claimsIdentity.FindFirst(ClaimTypes.Surname).Value;

            var userPictureId = await UploadProviderImageAsync(claimsIdentity, _organization);

            return new ApplicationUser
            {
                FirstName = userFirstName,
                LastName = userLastName,
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                OrganizationId = _organization.Id,
                PictureId = userPictureId
            };
        }

        private async Task<string> UploadProviderImageAsync(ClaimsIdentity claimsIdentity, Organization organization)
        {
            try
            {
                var imageBytes = await DownloadProviderImageAsync(claimsIdentity);
                var memoryStream = new MemoryStream(imageBytes);
                var imageName = $"{Guid.NewGuid()}.jpg";

                return await _pictureService.UploadFromStreamAsync(memoryStream, WebApiConstants.ImageMimeType, imageName, organization.Id);
            }
            catch
            {
                return null;
            }
        }

        private static async Task<byte[]> DownloadProviderImageAsync(ClaimsIdentity claimsIdentity)
        {
            var imageUrl = claimsIdentity.FindFirst(WebApiConstants.ClaimPicture)?.Value;

            if (imageUrl == null)
            {
                return null;
            }

            return await new WebClient().DownloadDataTaskAsync(imageUrl);
        }

        private async Task<ExternalProviderResult> ExecuteExternalLoginStrategyAsync()
        {
            return await new ExternalLoginStrategy(_cookieService, _tokenService, _externalLoginInfo).ExecuteStrategyAsync();
        }
    }
}
