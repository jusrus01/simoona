using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Users;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalProviderLinkAccountStrategy : IExternalProviderStrategy
    {
        private readonly ExternalLoginRequestDto _requestDto;
        private readonly ExternalLoginInfo _externalLoginInfo;

        private readonly bool _restoreUser;

        private readonly IApplicationUserManager _userManager;

        public ExternalProviderLinkAccountStrategy(
            IApplicationUserManager userManager,
            ExternalLoginRequestDto requestDto,
            ExternalLoginInfo externalLoginInfo,
            bool restoreUser = false)
        {
            _userManager = userManager;

            _externalLoginInfo = externalLoginInfo;
            _requestDto = requestDto;
            _restoreUser = restoreUser;
        }

        public async Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var userToLink = await GetLinkableUserAsync();

            await _userManager.AddLoginAsync(userToLink, _externalLoginInfo);
            
            return new ExternalProviderResult();
        }

        private async Task<ApplicationUser> GetLinkableUserAsync()
        {
            return _restoreUser ? 
                await _userManager.RestoreSoftDeletedUserByIdAsync(_requestDto.UserId) :
                await _userManager.FindByIdAsync(_requestDto.UserId);
        }
    }
}
