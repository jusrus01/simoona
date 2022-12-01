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

        public override void CheckIfRequiredParametersAreSet()
        {
            EnsureParametersAreSet(Parameters.LoginInfo, Parameters.RestoreUser, Parameters.Request);
        }

        public override async Task<ExternalProviderResult> ExecuteAsync()
        {
            var userToLink = await GetLinkableUserAsync();
            await _userManager.AddLoginAsync(userToLink, Parameters.LoginInfo);
            return new ExternalProviderResult();
        }

        private async Task<ApplicationUser> GetLinkableUserAsync()
        {
            return Parameters.RestoreUser.Value ? 
                await _userManager.RestoreSoftDeletedUserByIdAsync(Parameters.Request.UserId) :
                await _userManager.FindByIdAsync(Parameters.Request.UserId);
        }
    }
}
