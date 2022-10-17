using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.EmailTemplateViewModels;
using Shrooms.Contracts.Infrastructure.Email;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.UserService;

namespace Shrooms.Domain.Services.Email.AdministrationUsers
{
    public class AdministrationUsersNotificationService : IAdministrationNotificationService
    {
        private readonly DbSet<Organization> _organizationDbSet;
        private readonly IMailingService _mailingService;
        private readonly ApplicationOptions _applicationOptions;
        private readonly IMailTemplate _mailTemplate;
        private readonly IUserService _userService;

        public AdministrationUsersNotificationService(
            IOptions<ApplicationOptions> applicationOptions,
            IUnitOfWork2 uow,
            IMailingService mailingService,
            IMailTemplate mailTemplate,
            IUserService permissionService)
        {
            _applicationOptions = applicationOptions.Value;

            _organizationDbSet = uow.GetDbSet<Organization>();

            _mailingService = mailingService;
            _mailTemplate = mailTemplate;
            _userService = permissionService;
        }

        public async Task SendConfirmedNotificationEmailAsync(string userEmail, UserAndOrganizationDto userAndOrg)
        {
            var organizationNameAndContent = await _organizationDbSet
                .Where(organization => organization.Id == userAndOrg.OrganizationId)
                .Select(organization => new { organization.ShortName, organization.WelcomeEmail })
                .FirstOrDefaultAsync();

            if (organizationNameAndContent == null)
            {
                return;
            }

            var mainPageUrl = _applicationOptions.ClientUrl;
            var userSettingsUrl = _applicationOptions.UserNotificationSettingsUrl(organizationNameAndContent.ShortName);
            var subject = string.Format(Resources.Common.NewUserConfirmedNotificationEmailSubject);

            var emailTemplateViewModel = new UserConfirmationEmailTemplateViewModel(userSettingsUrl, mainPageUrl, organizationNameAndContent.WelcomeEmail);

            var body = _mailTemplate.Generate(emailTemplateViewModel);

            await _mailingService.SendEmailAsync(new EmailDto(userEmail, subject, body));
        }

        public async Task NotifyAboutNewUserAsync(ApplicationUser newUser, int orgId)
        {
            var userAdministrationEmails = await _userService.GetUserEmailsWithPermissionAsync(AdministrationPermissions.ApplicationUser, orgId);

            if (!userAdministrationEmails.Any())
            {
                return;
            }

            var organizationName = await _organizationDbSet
                .Where(organization => organization.Id == orgId)
                .Select(organization => organization.ShortName)
                .FirstOrDefaultAsync();

            var newUserProfileUrl = _applicationOptions.UserProfileUrl(organizationName, newUser.Id);
            var userSettingsUrl = _applicationOptions.UserNotificationSettingsUrl(organizationName);
            var subject = string.Format(Resources.Common.NewUserConfirmEmailSubject);

            var emailTemplateViewModel = new NotificationAboutNewUserEmailTemplateViewModel(userSettingsUrl, newUserProfileUrl, newUser.FullName);

            var body = _mailTemplate.Generate(emailTemplateViewModel);

            var emailDto = new EmailDto(userAdministrationEmails, subject, body);
            await _mailingService.SendEmailAsync(emailDto);
        }

        public async Task SendUserResetPasswordEmailAsync(ApplicationUser user, string token, string organizationName)
        {
            var userSettingsUrl = _applicationOptions.UserNotificationSettingsUrl(organizationName);
            var resetUrl = _applicationOptions.ResetPasswordUrl(organizationName, user.UserName, token);

            var resetPasswordTemplateViewModel = new ResetPasswordTemplateViewModel(user.FullName, userSettingsUrl, resetUrl);
            var subject = string.Format(Resources.Common.UserResetPasswordEmailSubject);
            var content = _mailTemplate.Generate(resetPasswordTemplateViewModel);

            await _mailingService.SendEmailAsync(new EmailDto(user.Email, subject, content));
        }

        public async Task SendUserVerificationEmailAsync(ApplicationUser user, string token, string organizationName)
        {
            var userSettingsUrl = _applicationOptions.UserNotificationSettingsUrl(organizationName);
            var verifyUrl = _applicationOptions.VerifyEmailUrl(organizationName, user.UserName, token);

            var verifyEmailTemplateViewModel = new VerifyEmailTemplateViewModel(user.FullName, userSettingsUrl, verifyUrl);
            var subject = string.Format(Resources.Common.UserVerifyEmailSubject);
            var content = _mailTemplate.Generate(verifyEmailTemplateViewModel);

            await _mailingService.SendEmailAsync(new EmailDto(user.Email, subject, content));
        }
    }
}
