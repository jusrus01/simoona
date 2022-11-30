using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.Models.Administration;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Domain.Services.Administration
{
    public interface IAdministrationUsersService
    {
        Task<ByteArrayContent> GetAllUsersExcelAsync(string fileName, int organizationId);

        Task ConfirmNewUserAsync(string userId, UserAndOrganizationDto userAndOrg);

        Task<IEnumerable<AdministrationUserDto>> GetAllUsersAsync(string sortQuery, string search, FilterDto[] filter, string includeProperties);

        void NotifyAboutNewUser(ApplicationUser user, int orgId);

        Task SetUserTutorialStatusToCompleteAsync(string userId);

        Task<bool> GetUserTutorialStatusAsync(string userId);

        Task SendUserPasswordResetEmailAsync(string email);

        Task<LoggedInUserInfoDto> GetUserInfoAsync(IIdentity identity);
    }
}
