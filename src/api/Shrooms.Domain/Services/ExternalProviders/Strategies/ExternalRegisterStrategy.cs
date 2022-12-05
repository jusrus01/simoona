using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Extensions;
using Shrooms.Domain.Services.ExternalProviders.Arguments;
using Shrooms.Domain.Services.Users;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalRegisterStrategy : ExternalProviderStrategyBase, IExternalProviderStrategy<RegisterArgs>
    {
        private RegisterArgs _arguments;

        private readonly IApplicationUserManager _userManager;
        private readonly IExternalPictureService _externalPictureService;

        public ExternalRegisterStrategy(IApplicationUserManager userManager, IExternalPictureService externalPictureService)
        {
            _userManager = userManager;
            _externalPictureService = externalPictureService;
        }

        public bool CanBeUsed(ExternalLoginInfo loginInfo, ExternalLoginRequestDto requestDto) =>
            loginInfo != null &&
            requestDto.IsRegistration &&
            requestDto.UserId == null;

        public void SetArguments(RegisterArgs arguments) =>
            _arguments = arguments;

        public void SetArguments(params object[] arguments) =>
            SetArguments(MapArgumentsToRequiredArgument<RegisterArgs>(
                arguments,
                typeof(ExternalLoginInfo),
                typeof(ExternalLoginRequestDto),
                typeof(Organization)));

        public async Task<ExternalProviderPartialResult> ExecuteAsync()
        {
            var claimsIdentity = _arguments.LoginInfo.Principal.Identity as ClaimsIdentity;
            var email = claimsIdentity.GetEmail();
            CheckIfEmailExists(email);

            if (await _userManager.IsUserSoftDeletedAsync(email))
            {
                return MoveToLinkingStrategy();
            }

            var user = await FindUserByEmailAsync(email);
            if (UserHasValidAccount(user))
            {
                return MoveToLoginStrategy();
            }

            if (user != null)
            {
                return MoveToRestoreStrategy();
            }
            
            await RegisterUserAsync(claimsIdentity);
            return MoveToLoginStrategy();
        }

        private async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch (ValidationException ex)
            {
                if (ex.ErrorCode != ErrorCodes.UserNotFound)
                {
                    throw;
                }

                return null;
            }
        }

        private async Task RegisterUserAsync(ClaimsIdentity claimsIdentity)
        {
            var userPictureId = await _externalPictureService.UploadProviderImageAsync(claimsIdentity);
            var newUser = ExternalProviderUserCreator.CreateApplicationUser(claimsIdentity, userPictureId, _arguments.Organization.Id);
            await _userManager.CreateAsync(newUser);
            await _userManager.AddLoginAsync(newUser, _arguments.LoginInfo);
        }

        private static void CheckIfEmailExists(string email) // TODO: Make sure this will work with facebook provider
        {
            if (email == null) 
            {
                throw new ValidationException(ErrorCodes.Unspecified, "External provider did not provide email");
            }
        }

        private static bool UserHasValidAccount(ApplicationUser user) =>
            user != null && user.EmailConfirmed;

        private ExternalProviderPartialResult MoveToRestoreStrategy() =>
            Next<ExternalLoginStrategy>(_arguments.LoginInfo, _arguments.Request);

        private ExternalProviderPartialResult MoveToLoginStrategy() =>
            Next<ExternalLoginStrategy>(_arguments.LoginInfo, _arguments.Request);

        private ExternalProviderPartialResult MoveToLinkingStrategy() =>
            Next<ExternalProviderLinkAccountStrategy>(_arguments.LoginInfo, _arguments.Request, true);
    }
}
