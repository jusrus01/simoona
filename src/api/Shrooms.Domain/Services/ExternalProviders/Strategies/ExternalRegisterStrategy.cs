using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Extensions;
using Shrooms.Domain.Services.Picture;
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
        private readonly IPictureService _pictureService;

        private readonly ExternalLoginStrategy _loginStrategy;
        private readonly ExternalProviderLinkAccountStrategy _linkAccountStrategy;

        public ExternalRegisterStrategy(
            ExternalLoginStrategy loginStrategy,
            ExternalProviderLinkAccountStrategy linkAccountStrategy,
            IApplicationUserManager userManager,
            IPictureService pictureService)
        {
            _loginStrategy = loginStrategy;
            _linkAccountStrategy = linkAccountStrategy;
            _userManager = userManager;
            _pictureService = pictureService;
        }

        public override void CheckIfRequiredParametersAreSet()
        {
            EnsureParametersAreSet(Parameters.LoginInfo, Parameters.OrganizationId, Parameters.Request);
        }

        public override async Task<ExternalProviderResult> ExecuteAsync()
        {
            var claimsIdentity = Parameters.LoginInfo.Principal.Identity as ClaimsIdentity;
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
            await _userManager.AddLoginAsync(newUser, Parameters.LoginInfo);
        }

        private async Task RestoreUserAsync(ApplicationUser user, ClaimsIdentity claimsIdentity)
        {
            var newUser = await CreateApplicationUserFromIdentityAsync(claimsIdentity, user.Email);

            await _userManager.RemoveLoginAsync(user, AuthenticationConstants.InternalLoginProvider, user.Id);
            await _userManager.RemovePasswordAsync(user);

            CopyUserValues(fromUser: newUser, toUser: user);

            await _userManager.UpdateAsync(user);
            await _userManager.AddLoginAsync(user, Parameters.LoginInfo);
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

            var userPictureId = await UploadProviderImageAsync(claimsIdentity);

            return new ApplicationUser
            {
                FirstName = userFirstName,
                LastName = userLastName,
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                OrganizationId = Parameters.OrganizationId.Value,
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

        private async Task<ExternalProviderResult> ExecuteExternalLoginStrategyAsync()
        {
            _loginStrategy.SetParameters(new ExternalProviderStrategyParameters(Parameters.LoginInfo));
            return await _loginStrategy.ExecuteStrategyAsync();
        }

        private async Task<ExternalProviderResult> ExecuteExternalAccountLinkingStrategy()
        {
            _linkAccountStrategy.SetParameters(new ExternalProviderStrategyParameters(Parameters, true));
            return await _linkAccountStrategy.ExecuteStrategyAsync();
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
