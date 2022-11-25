using System.Threading.Tasks;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Domain.Services.Email.AdministrationUsers
{
    public interface IAdministrationNotificationService
    {
        Task NotifyAboutNewUserAsync(ApplicationUser newUser, int orgId);

        Task SendConfirmedNotificationEmailAsync(string userEmail, UserAndOrganizationDto userAndOrg);
    }
}
