using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.ExternalProviders.Arguments;
using Shrooms.Domain.Services.Users;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalRestoreStrategy : ExternalProviderStrategyBase, IExternalProviderStrategy<RestoreArgs>
    {
        private RestoreArgs _arguments;

        private readonly IApplicationUserManager _userManager;
        private readonly IExternalPictureService _externalPictureService;

        public ExternalRestoreStrategy(IApplicationUserManager userManager, IExternalPictureService externalPictureService)
        {
            _userManager = userManager;
            _externalPictureService = externalPictureService;
        }

        public bool CanBeUsed(ExternalLoginInfo loginInfo, ExternalLoginRequestDto requestDto) =>
            ExcludeFromFactorySearch();

        public async Task<ExternalProviderPartialResult> ExecuteAsync()
        {
            var newUser = await CreateNewUserAsync();
            await AddNewLoginAsync(newUser);
            return Next<ExternalLoginStrategy>(new LoginArgs(_arguments.LoginInfo, _arguments.Request));
        }

        public void SetArguments(RestoreArgs arguments) =>
            _arguments = arguments;

        public void SetArguments(params object[] arguments) =>
            SetArguments(MapArgumentsToRequiredArgument<RestoreArgs>(
                arguments,
                typeof(ExternalLoginInfo),
                typeof(ExternalLoginRequestDto),
                typeof(ApplicationUser),
                typeof(ClaimsIdentity),
                typeof(int)));

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

        private async Task<ApplicationUser> CreateNewUserAsync()
        {
            var pictureId = await _externalPictureService.UploadProviderImageAsync(_arguments.Identity);
            var newUser = ExternalProviderUserCreator.CreateApplicationUser(_arguments.Identity, pictureId, _arguments.OrganizationId);
            return newUser;
        }

        private async Task AddNewLoginAsync(ApplicationUser newUser)
        {
            await _userManager.RemoveLoginAsync(_arguments.RestorableUser, AuthenticationConstants.InternalLoginProvider, _arguments.RestorableUser.Id);
            await _userManager.RemovePasswordAsync(_arguments.RestorableUser);
            CopyUserValues(fromUser: newUser, toUser: _arguments.RestorableUser);
            await _userManager.UpdateAsync(_arguments.RestorableUser);
            await _userManager.AddLoginAsync(_arguments.RestorableUser, _arguments.LoginInfo);
        }
    }
}
