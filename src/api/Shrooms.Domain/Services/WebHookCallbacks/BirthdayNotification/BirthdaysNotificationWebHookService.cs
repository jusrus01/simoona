using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.EmailTemplateViewModels;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Infrastructure.Email;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Roles;

namespace Shrooms.Domain.Services.WebHookCallbacks.BirthdayNotification
{
    public class BirthdaysNotificationWebHookService : IBirthdaysNotificationWebHookService
    {
        private readonly DbSet<ApplicationUser> _usersDbSet;
        private readonly DbSet<Organization> _organizationsDbSet;
        private readonly DateTime _date;
        private readonly IMailService _mailingService;
        private readonly IRoleService _roleService;
        private readonly IMailTemplate _mailTemplate;
        private readonly ApplicationOptions _applicationOptions;

        public BirthdaysNotificationWebHookService(IUnitOfWork2 uow,
            IMailService mailingService,
            IRoleService roleService,
            IMailTemplate mailTemplate,
            IOptions<ApplicationOptions> applicationOptions)
        {
            _usersDbSet = uow.GetDbSet<ApplicationUser>();
            _organizationsDbSet = uow.GetDbSet<Organization>();

            _date = DateTime.UtcNow;
            _mailingService = mailingService;
            _roleService = roleService;
            _mailTemplate = mailTemplate;
            _applicationOptions = applicationOptions.Value;
        }

        public async Task SendNotificationsAsync(string organizationName)
        {
            var todaysBirthdayUsers = await _usersDbSet
                .Where(w =>
                    w.BirthDay.HasValue &&
                    w.BirthDay.Value.Month == _date.Month &&
                    w.BirthDay.Value.Day == _date.Day)
                .ToListAsync();

            if (!todaysBirthdayUsers.Any())
            {
                return;
            }

            await SendBirthdayReminderAsync(todaysBirthdayUsers, organizationName);
        }

        private async Task SendBirthdayReminderAsync(IEnumerable<ApplicationUser> employees, string organizationName)
        {
            var currentOrganization = await _organizationsDbSet.FirstAsync(name => name.ShortName == organizationName);

            var receivers = await _roleService.GetAdministrationRoleEmailsAsync(currentOrganization.Id);
            var model = new BirthdaysNotificationTemplateViewModel(GetFormattedEmployeesList(employees, organizationName, currentOrganization.ShortName),
                _applicationOptions.UserNotificationSettingsUrl(organizationName));
            var content = _mailTemplate.Generate(model);
            var emailData = new EmailDto(receivers, Resources.Emails.Templates.BirthdaysNotificationEmailSubject, content);

            await _mailingService.SendEmailAsync(emailData);
        }

        private IList<BirthdaysNotificationEmployeeViewModel> GetFormattedEmployeesList(IEnumerable<ApplicationUser> employees, string organizationName, string tenantPicturesContainer)
        {
            var formattedEmployeeList = new List<BirthdaysNotificationEmployeeViewModel>();

            foreach (var employee in employees)
            {
                formattedEmployeeList.Add(new BirthdaysNotificationEmployeeViewModel
                {
                    FullName = $"{employee.FirstName} {employee.LastName}",
                    PictureUrl = _applicationOptions.PictureUrl(tenantPicturesContainer, employee.PictureId),
                    ProfileUrl = _applicationOptions.UserProfileUrl(organizationName, employee.Id)
                });
            }

            return formattedEmployeeList;
        }
    }
}
