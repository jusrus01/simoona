using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.Models.Support;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Infrastructure.Email;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Domain.Services.Support
{
    public class SupportService : ISupportService
    {
        private readonly DbSet<ApplicationUser> _applicationUsers;
        private readonly IMailService _mailingService;
        private readonly ApplicationOptions _applicationOptions;

        public SupportService(IUnitOfWork2 uow, IMailService mailingService, IOptions<ApplicationOptions> applicationOptions)
        {
            _mailingService = mailingService;
            _applicationOptions = applicationOptions.Value;
            _applicationUsers = uow.GetDbSet<ApplicationUser>();
        }

        public async Task SubmitTicketAsync(UserAndOrganizationDto userAndOrganization, SupportDto support)
        {
            var currentApplicationUser = await _applicationUsers.SingleAsync(u => u.Id == userAndOrganization.UserId);

            var email = new EmailDto(currentApplicationUser.FullName, currentApplicationUser.Email, _applicationOptions.SupportEmail, $"{support.Type}: {support.Subject}", support.Message);

            await _mailingService.SendEmailAsync(email, true);
        }
    }
}
