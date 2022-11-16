using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Email.InternalProviders;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Users;
using Shrooms.Infrastructure.FireAndForget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.InternalProviders
{
    public class InternalProviderService : IInternalProviderService
    {
        private readonly IApplicationUserManager _userManager;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly IOrganizationService _organizationService;
        private readonly IInternalProviderNotificationService _notificationService;
        private readonly ICookieService _cookieService;

        public InternalProviderService(
            IApplicationUserManager userManager,
            ITenantNameContainer tenantNameContainer,
            IOrganizationService organizationService,
            ICookieService cookieService,
            IInternalProviderNotificationService notificationService)
        {
            _userManager = userManager;
            _tenantNameContainer = tenantNameContainer;
            _organizationService = organizationService;
            _cookieService = cookieService;
            _notificationService = notificationService;
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            if (!await _userManager.UserExistsAsync(registerDto.Email))
            {
                await RegisterNewUserAsync(registerDto);
            }
            else if (!await _userManager.IsUserSoftDeletedAsync(registerDto.Email))
            {
                await RestoreUserAsync(registerDto);
            }
            else
            {
                await OverrideUnconfirmedUserAsync(registerDto);
            }
        }

        public async Task<IEnumerable<ExternalLoginDto>> GetLoginsAsync()
        {
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);
            
            return _organizationService.HasProvider(organization, AuthenticationConstants.InternalLoginProvider) ?
                new List<ExternalLoginDto>
                {
                    new ExternalLoginDto
                    {
                        Name = AuthenticationConstants.InternalLoginProvider,
                    }
                } :
                Enumerable.Empty<ExternalLoginDto>();
        }

        public async Task SetSignInCookieAsync()
        {
            await _cookieService.SetExternalCookieAsync();
        }

        private async Task RegisterNewUserAsync(RegisterDto registerDto)
        {
            var tenantName = _tenantNameContainer.TenantName;
            var organization = await _organizationService.GetOrganizationByNameAsync(tenantName);

            var newUser = CreateNewApplicationUser(registerDto, organization);

            await _userManager.CreateAsync(newUser, registerDto.Password);

            await AddInternalLoginAsync(newUser);

            await AddNewUserRolesAsync(newUser);

            await SendConfirmationEmailAsync(newUser);
        }

        private async Task RestoreUserAsync(RegisterDto registerDto)
        {
            var user = await _userManager.RestoreSoftDeletedUserByEmailAsync(registerDto.Email);

            await AddNewUserRolesAsync(user);
        }

        private async Task AddNewUserRolesAsync(ApplicationUser user)
        {
            await _userManager.AddToRoleAsync(user, Contracts.Constants.Roles.NewUser);
            await _userManager.AddToRoleAsync(user, Contracts.Constants.Roles.FirstLogin);
        }

        private async Task AddInternalLoginAsync(ApplicationUser newUser)
        {
            var internalLogin = new UserLoginInfo(
                  AuthenticationConstants.InternalLoginProvider,
                  providerKey: newUser.Id,
                  AuthenticationConstants.InternalLoginProvider);

            await _userManager.AddLoginAsync(newUser, internalLogin);
        }

        private async Task OverrideUnconfirmedUserAsync(RegisterDto registerDto)
        {
            // Possible unexpected behavior when a user tries to register for another organization with the same email address
            // when organizations are hosted on the same database instance (at the moment we keep organization's databases instances separate)
            var user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user.EmailConfirmed || await _userManager.HasExternalLoginAsync(user))
            {
                throw new ValidationException(ErrorCodes.DuplicatesIntolerable, "User is already registered");
            }

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, registerDto.Password);

            await SendConfirmationEmailAsync(user);
        }

        private async Task SendConfirmationEmailAsync(ApplicationUser user)
        {
            // TODO: use async runner
            await _notificationService.SendConfirmationEmailAsync(user);
        }

        private static ApplicationUser CreateNewApplicationUser(RegisterDto registerDto, Organization organization)
        {
            return new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                EmploymentDate = DateTime.UtcNow,
                CultureCode = organization.CultureCode ?? BusinessLayerConstants.DefaultCulture,
                TimeZone = organization.TimeZone,
                NotificationsSettings = null,
                OrganizationId = organization.Id,
            };
        }

    }
}
