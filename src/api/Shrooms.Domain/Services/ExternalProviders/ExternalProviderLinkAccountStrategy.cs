using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalProviderLinkAccountStrategy : IExternalProviderStrategy
    {
        private readonly ExternalLoginRequestDto _requestDto;
        private readonly ExternalLoginInfo _externalLoginInfo;
        private readonly bool _restoreUser;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork2 _uow;

        public ExternalProviderLinkAccountStrategy(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork2 uow,
            ExternalLoginRequestDto requestDto,
            ExternalLoginInfo externalLoginInfo,
            bool restoreUser = false)
        {
            _uow = uow;
            _userManager = userManager;

            _externalLoginInfo = externalLoginInfo;
            _requestDto = requestDto;
            _restoreUser = restoreUser;
        }

        public async Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            var userToLink = await GetLinkableUserAsync();

            if (userToLink == null)
            {
                throw new ValidationException(ErrorCodes.UserNotFound, "User not found");
            }

            var identityResult = await _userManager.AddLoginAsync(userToLink, _externalLoginInfo);

            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to add login");
            }

            return new ExternalProviderResult();
        }

        private async Task<ApplicationUser> GetLinkableUserAsync()
        {
            if (_restoreUser)
            {
                return await _uow.GetDbSet<ApplicationUser>()
                    .FromSql($"UPDATE[dbo].[AspNetUsers] SET[IsDeleted] = '0' WHERE Id = {_requestDto.UserId}") // Bad
                    .SingleAsync();
            }

            return await _userManager.FindByIdAsync(_requestDto.UserId);
        }
    }
}
