using Shrooms.DataLayer.EntityModels.Models;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Email.InternalProviders
{
    public interface IInternalProviderNotificationService
    {
        Task SendConfirmationEmailAsync(ApplicationUser user);
    }
}