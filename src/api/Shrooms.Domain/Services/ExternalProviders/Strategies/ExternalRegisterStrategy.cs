using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.ExternalProviders;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Extensions;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Picture;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Domain.Services.Users;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalRegisterStrategy : ExternalProviderStrategyBase
    {
        private readonly IApplicationUserManager _userManager;
        private readonly ICookieService _cookieService;
        private readonly ITokenService _tokenService;
        private readonly IPictureService _pictureService;

        public ExternalRegisterStrategy(
            ITokenService tokenService,
            IApplicationUserManager userManager,
            ICookieService cookieService,
            IPictureService pictureService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _cookieService = cookieService;
            _pictureService = pictureService;
        }

        public override void EnsureValidParameters(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null)
        {
            EnsureParametersAreSet(loginInfo, parameters.OrganizationId, parameters.Request);
        }

        public override async Task<ExternalProviderResult> ExecuteAsync(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null)
        {
            var claimsIdentity = loginInfo.Principal.Identity as ClaimsIdentity;
            var email = claimsIdentity.GetEmail();

            CheckIfEmailExists(email);

            if (await _userManager.IsUserSoftDeletedAsync(email))
            {
                return await ExecuteExternalAccountLinkingStrategy(parameters, loginInfo);
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user.EmailConfirmed)
                {
                    return await ExecuteExternalLoginStrategyAsync(loginInfo);
                }

                await RestoreUserAsync(loginInfo, parameters, user, claimsIdentity);
            }
            catch (ValidationException ex)
            {
                if (ex.ErrorCode != ErrorCodes.UserNotFound)
                {
                    throw;
                }

                await RegisterUserAsync(loginInfo, parameters, claimsIdentity, email);
            }
            
            return await ExecuteExternalLoginStrategyAsync(loginInfo);
        }

        private async Task RegisterUserAsync(ExternalLoginInfo loginInfo, ExternalProviderStrategyParametersDto parameters, ClaimsIdentity claimsIdentity, string userEmail)
        {
            var newUser = await CreateApplicationUserFromIdentityAsync(claimsIdentity, userEmail, parameters.OrganizationId.Value);

            await _userManager.CreateAsync(newUser);
            await _userManager.AddLoginAsync(newUser, loginInfo);
        }

        private async Task RestoreUserAsync(ExternalLoginInfo loginInfo, ExternalProviderStrategyParametersDto parameters, ApplicationUser user, ClaimsIdentity claimsIdentity)
        {
            var newUser = await CreateApplicationUserFromIdentityAsync(claimsIdentity, user.Email, parameters.OrganizationId.Value);

            await _userManager.RemoveLoginAsync(user, AuthenticationConstants.InternalLoginProvider, user.Id);
            await _userManager.RemovePasswordAsync(user);

            CopyUserValues(fromUser: newUser, toUser: user);

            await _userManager.UpdateAsync(user);
            await _userManager.AddLoginAsync(user, loginInfo);
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

        private async Task<ApplicationUser> CreateApplicationUserFromIdentityAsync(ClaimsIdentity claimsIdentity, string email, int organizationId)
        {
            var userFirstName = claimsIdentity.GetFirstName();
            var userLastName = claimsIdentity.GetLastName();

            var userPictureId = await UploadProviderImageAsync(claimsIdentity);

            return new ApplicationUser
            {
                FirstName = userFirstName,
                LastName = userLastName,
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                OrganizationId = organizationId,
                PictureId = userPictureId
            };
        }

        private async Task<string> UploadProviderImageAsync(ClaimsIdentity claimsIdentity)
        {
            try
            {
                var imageBytes = await DownloadProviderImageAsync(claimsIdentity);
                var memoryStream = new MemoryStream(imageBytes);
                return await _pictureService.UploadFromStreamAsync(memoryStream, MimeTypeConstants.Jpeg);
            }
            catch
            {
                // Do not allow an exception to bubble up because failure to retrieve the provider image should not cancel the registration flow.
                // Furthermore, a default image exists for a user, and in the user confirmation part, the user is required to set an image himself if one is not provided.
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

        private async Task<ExternalProviderResult> ExecuteExternalLoginStrategyAsync(ExternalLoginInfo loginInfo)
        {
            return await new ExternalLoginStrategy(_cookieService, _tokenService).ExecuteStrategyAsync(null, loginInfo);
        }

        private async Task<ExternalProviderResult> ExecuteExternalAccountLinkingStrategy(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo)
        {
            var newParameters = new ExternalProviderStrategyParametersDto(parameters, true);
            return await new ExternalProviderLinkAccountStrategy(_userManager).ExecuteStrategyAsync(newParameters, loginInfo);
        }

        private static void CheckIfEmailExists(string email) // TODO: Make sure this will work with facebook provider
        {
            if (email == null) 
            {
                throw new ValidationException(ErrorCodes.Unspecified, "External provider did not provide email");
            }
        }
    }
}
