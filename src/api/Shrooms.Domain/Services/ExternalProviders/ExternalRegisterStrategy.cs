using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Extensions;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Picture;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Domain.Services.Users;
using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalRegisterStrategy : IExternalProviderStrategy
    {
        private readonly IApplicationUserManager _userManager;

        private readonly ICookieService _cookieService;
        private readonly ExternalLoginInfo _externalLoginInfo;
        private readonly ITokenService _tokenService;
        private readonly IPictureService _pictureService;
        private readonly ExternalLoginRequestDto _requestDto;
        private readonly Organization _organization;

        public ExternalRegisterStrategy(
            ITokenService tokenService,
            IApplicationUserManager userManager,
            ICookieService cookieService,
            IPictureService pictureService,
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
            _organization = organization;
        }
        
        public async Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var claimsIdentity = _externalLoginInfo.Principal.Identity as ClaimsIdentity;
            var email = claimsIdentity.GetEmail();

            CheckIfEmailExists(email);

            if (await _userManager.IsUserSoftDeletedAsync(email))
            {
                return await ExecuteExternalAccountLinkingStrategy();
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user.EmailConfirmed)
                {
                    return await ExecuteExternalLoginStrategyAsync();
                }

                await RestoreUserAsync(user, claimsIdentity);
            }
            catch (ValidationException ex)
            {
                if (ex.ErrorCode != ErrorCodes.UserNotFound)
                {
                    throw;
                }

                await RegisterUserAsync(claimsIdentity, email);
            }
            
            return await ExecuteExternalLoginStrategyAsync();
        }

        private async Task RegisterUserAsync(ClaimsIdentity claimsIdentity, string userEmail)
        {
            var newUser = await CreateApplicationUserFromIdentityAsync(claimsIdentity, userEmail);

            await _userManager.CreateAsync(newUser);
            await _userManager.AddLoginAsync(newUser, _externalLoginInfo);
        }

        private async Task RestoreUserAsync(ApplicationUser user, ClaimsIdentity claimsIdentity)
        {
            var newUser = await CreateApplicationUserFromIdentityAsync(claimsIdentity, user.Email);

            await _userManager.RemoveLoginAsync(user, AuthenticationConstants.InternalLoginProvider, user.Id);
            await _userManager.RemovePasswordAsync(user);

            CopyUserValues(fromUser: newUser, toUser: user);

            await _userManager.UpdateAsync(user);
            await _userManager.AddLoginAsync(user, _externalLoginInfo);
        }

        private static void CopyUserValues(ApplicationUser fromUser, ApplicationUser toUser)
        {
            toUser.FirstName = fromUser.FirstName;
            toUser.LastName = fromUser.LastName;
            toUser.Email = fromUser.Email;
            toUser.UserName = fromUser.UserName;
            toUser.EmailConfirmed = fromUser.EmailConfirmed;
            toUser.OrganizationId = fromUser.OrganizationId;
            toUser.PictureId = fromUser.PictureId;
        }

        private async Task<ApplicationUser> CreateApplicationUserFromIdentityAsync(ClaimsIdentity claimsIdentity, string email)
        {
            var userFirstName = claimsIdentity.GetFirstName();
            var userLastName = claimsIdentity.GetLastName();

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
            var imageUrl = claimsIdentity.GetGooglePictureUrl();

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

        private async Task<ExternalProviderResult> ExecuteExternalAccountLinkingStrategy()
        {
            return await new ExternalProviderLinkAccountStrategy(
                _userManager,
                _requestDto,
                _externalLoginInfo,
                restoreUser: true)
                .ExecuteStrategyAsync();
        }

        private static void CheckIfEmailExists(string email) // TODO: Test it with Facebook provider and export
        {
            if (email == null) 
            {
                throw new ValidationException(ErrorCodes.Unspecified, "External provider did not provide email");
            }
        }
    }
}
