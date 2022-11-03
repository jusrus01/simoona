using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.Infrastructure.Email;
using Shrooms.Contracts.Options;
using System.Threading.Tasks;

namespace Shrooms.Infrastructure.Email
{
    public class Smpt4DevMailService : IMailService
    {
        private readonly MailOptions _options;

        public Smpt4DevMailService(IOptions<MailOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendEmailAsync(EmailDto email, bool skipDomainChange = false)
        {
            using var client = new SmtpClient();

            await client.ConnectAsync(_options.Host, _options.Port.Value, SecureSocketOptions.None);

            client.Capabilities &= ~SmtpCapabilities.Pipelining;

            await client.SendAsync(BuildMessage(email));
            await client.DisconnectAsync(true);
        }

        private static MimeMessage BuildMessage(EmailDto email)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress(email.SenderFullName, email.SenderEmail));

            foreach (var receiver in email.Receivers)
            {
                mimeMessage.To.Add(new MailboxAddress("", receiver));
            }

            mimeMessage.Subject = email.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = email.Body
            };

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            return mimeMessage;
        }
    }
}
