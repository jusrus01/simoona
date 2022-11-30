using Microsoft.Extensions.Options;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.EmailTemplateViewModels;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Infrastructure.Email;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Email.InternalProviders
{
    public class InternalProviderNotificationService : IInternalProviderNotificationService
    {
        private readonly ApplicationOptions _applicationOptions;
        private readonly IMailService _mailService;
        private readonly IMailTemplate _mailTemplate;
        private readonly ITenantNameContainer _tenantNameContainer;

        public InternalProviderNotificationService(
            IOptions<ApplicationOptions> applicationOptions,
            IMailTemplate mailTemplate,
            IMailService mailService,
            ITenantNameContainer tenantNameContainer)
        {
            _applicationOptions = applicationOptions.Value;
            
            _mailService = mailService;
            _mailTemplate = mailTemplate;
            _tenantNameContainer = tenantNameContainer;
        }
        
        public async Task SendResetPasswordEmailAsync(ApplicationUser user, string token)
        {
            var userSettingsUrl = _applicationOptions.UserNotificationSettingsUrl(_tenantNameContainer.TenantName);
            var resetUrl = _applicationOptions.ResetPasswordUrl(_tenantNameContainer.TenantName, user.UserName, token);

            var resetPasswordTemplateViewModel = new ResetPasswordTemplateViewModel(user.FullName, userSettingsUrl, resetUrl);

            var subject = string.Format(Resources.Common.UserResetPasswordEmailSubject);
            var content = _mailTemplate.Generate(resetPasswordTemplateViewModel);

            await _mailService.SendEmailAsync(new EmailDto(user.Email, subject, content));
        }

        public async Task SendUserVerificationEmailAsync(ApplicationUser user, string token)
        {
            var userSettingsUrl = _applicationOptions.UserNotificationSettingsUrl(_tenantNameContainer.TenantName);
            var verifyUrl = _applicationOptions.VerifyEmailUrl(_tenantNameContainer.TenantName, user.UserName, token);

            var verifyEmailTemplateViewModel = new VerifyEmailTemplateViewModel(user.FullName, userSettingsUrl, verifyUrl);
            var subject = string.Format(Resources.Common.UserVerifyEmailSubject);
            var content = _mailTemplate.Generate(verifyEmailTemplateViewModel);

            await _mailService.SendEmailAsync(new EmailDto(user.Email, subject, content));
        }
    }
}
