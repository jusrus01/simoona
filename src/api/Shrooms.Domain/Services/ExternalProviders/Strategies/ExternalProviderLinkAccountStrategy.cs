using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.ExternalProviders;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Users;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public class ExternalProviderLinkAccountStrategy : ExternalProviderStrategyBase
    {
        private readonly IApplicationUserManager _userManager;

        public ExternalProviderLinkAccountStrategy(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public override void EnsureValidParameters(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null)
        {
            EnsureParametersAreSet(loginInfo, parameters.RestoreUser, parameters.Request);
        }

        public override async Task<ExternalProviderResult> ExecuteAsync(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null)
        {
            var userToLink = await GetLinkableUserAsync(parameters);
            await _userManager.AddLoginAsync(userToLink, loginInfo);
            return new ExternalProviderResult();
        }

        private async Task<ApplicationUser> GetLinkableUserAsync(ExternalProviderStrategyParametersDto parameters)
        {
            return parameters.RestoreUser.Value ? 
                await _userManager.RestoreSoftDeletedUserByIdAsync(parameters.Request.UserId) :
                await _userManager.FindByIdAsync(parameters.Request.UserId);
        }
    }
}
