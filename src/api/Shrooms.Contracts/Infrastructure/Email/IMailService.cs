using System.Threading.Tasks;
using Shrooms.Contracts.DataTransferObjects;

namespace Shrooms.Contracts.Infrastructure.Email
{
    public interface IMailService
    {
        Task SendEmailAsync(EmailDto email, bool skipDomainChange = false);
    }
}
