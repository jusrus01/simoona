using Shrooms.DataLayer.EntityModels.Models;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Email.InternalProviders
{
    public interface IInternalProviderNotificationService
    {
        Task SendUserWasConfirmedEmailAsync(ApplicationUser user);

        Task SendUserVerificationEmailAsync(ApplicationUser user, string token);

        Task SendResetPasswordEmailAsync(ApplicationUser user, string token);
    }
}