using Microsoft.AspNetCore.Identity;
using Shrooms.Authentication.External.Arguments;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Users;
using System.Threading.Tasks;

namespace Shrooms.Authentication.External.Strategies
{
    public class ExternalProviderLinkAccountStrategy : ExternalProviderStrategyBase, IExternalProviderStrategy<LinkingArgs>
    {
        private LinkingArgs _arguments;

        private readonly IApplicationUserManager _userManager;

        public ExternalProviderLinkAccountStrategy(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public bool CanBeUsed(ExternalLoginInfo loginInfo, ExternalLoginRequestDto requestDto) =>
            loginInfo != null &&
            requestDto.UserId != null;

        public async Task<ExternalProviderPartialResult> ExecuteAsync()
        {
            var userToLink = await GetLinkableUserAsync();
            await _userManager.AddLoginAsync(userToLink, _arguments.LoginInfo);
            return Complete(new ExternalProviderResult());
        }

        public void SetArguments(LinkingArgs arguments) =>
            _arguments = arguments;

        public void SetArguments(params object[] arguments) =>
            SetArguments(MapArgumentsToRequiredArgument<LinkingArgs>(
                arguments,
                typeof(ExternalLoginInfo),
                typeof(ExternalLoginRequestDto),
                typeof(bool?)));

        private async Task<ApplicationUser> GetLinkableUserAsync()
        {
            return _arguments.RestoreUser.Value ? 
                await _userManager.RestoreSoftDeletedUserByIdAsync(_arguments.Request.UserId) :
                await _userManager.FindByIdAsync(_arguments.Request.UserId);
        }
    }
}
