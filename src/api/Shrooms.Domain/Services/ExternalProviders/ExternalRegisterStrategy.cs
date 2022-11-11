using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
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
    /// 
    /// It behaves a little differently than the old version,
    /// since it seems to me that most of the operations in there are
    /// quite useless. Change implementation if we cannot mimic the
    /// required behavior without them.
    /// 
    /// There is something with provider pictures,
    /// although I am not sure why they exist.
    /// 
    /// Edit: we download existing picture fron the provider and then set it.
    /// Edit 2: this strategy needs to handle the same cases that register internal does
    /// Edit 3: need to download stuff from external providers.
    /// Edit 4: handle the case when internal login is not confirmed and we try to register
    /// with external provider.
    /// 
    /// Q: do i really need to mimic that sign in flow? only redirects change.
    /// Q: how do we know by which email to send emails? (when we have multiple binded?) should that be restricted?
    /// </summary>
    public class ExternalRegisterStrategy : IExternalProviderStrategy
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ICookieService _cookieService;
        private readonly ExternalLoginInfo _externalLoginInfo;
        private readonly ITokenService _tokenService;
        private readonly IOrganizationService _organizationService;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly IPictureService _pictureService;

        public ExternalRegisterStrategy(
            ITokenService tokenService,
            ExternalLoginInfo externalLoginInfo,
            UserManager<ApplicationUser> userManager,
            IOrganizationService organizationService,
            ITenantNameContainer tenantNameContainer,
            ICookieService cookieService,
            IPictureService pictureService)
        {
            _userManager = userManager;

            _externalLoginInfo = externalLoginInfo;
            _tokenService = tokenService;
            _organizationService = organizationService;
            _tenantNameContainer = tenantNameContainer;
            _cookieService = cookieService;
            _pictureService = pictureService;
        }

        public async Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var claimsIdentity = _externalLoginInfo.Principal.Identity as ClaimsIdentity;
            
            var userEmail = claimsIdentity.FindFirst(ClaimTypes.Email).Value;

            if (userEmail == null) // TODO: Test it with Facebook provider
            {
                throw new ValidationException(ErrorCodes.Unspecified, "External provider did not provide email");
            }

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user != null)
            {
                return await ExecuteExternalLoginStrategyAsync();
            }

            // TODO: Fill in more, retrieve from claims and validate from another provider
            // TODO: Make sure that the email will be filled in by another service call
            user = await CreateNewUserAsync(claimsIdentity, userEmail);

            var identityResult = await _userManager.CreateAsync(user, userEmail);
            
            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to create user");
            }

            identityResult = await _userManager.AddLoginAsync(user, _externalLoginInfo);

            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to add login");
            }

            return await ExecuteExternalLoginStrategyAsync();
        }

        private async Task<ApplicationUser> CreateNewUserAsync(ClaimsIdentity claimsIdentity, string email)
        {
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);

            var userFirstName = claimsIdentity.FindFirst(ClaimTypes.GivenName).Value;
            var userLastName = claimsIdentity.FindFirst(ClaimTypes.Surname).Value;

            var userPictureId = await UploadProviderImageAsync(claimsIdentity, organization);

            return new ApplicationUser
            {
                FirstName = userFirstName,
                LastName = userLastName,
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                OrganizationId = organization.Id,
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

        private async Task<byte[]> DownloadProviderImageAsync(ClaimsIdentity claimsIdentity)
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
