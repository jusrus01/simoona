using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Infrastructure.FireAndForget;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Cookies;
using Shrooms.Domain.Services.Email.InternalProviders;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Users;
using Shrooms.Domain.ServiceValidators.Validators.InternalProviders;
using Shrooms.Infrastructure.FireAndForget;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.InternalProviders
{
    public class InternalProviderService : IInternalProviderService
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly IOrganizationService _organizationService;
        private readonly IInternalProviderValidator _validator;
        private readonly ICookieService _cookieService;
        private readonly IFireAndForgetScheduler _fireAndForgetScheduler;

        public InternalProviderService(
            IApplicationUserManager userManager,
            IApplicationRoleManager roleManager,
            ITenantNameContainer tenantNameContainer,
            IOrganizationService organizationService,
            ICookieService cookieService,
            IInternalProviderValidator validator,
            IFireAndForgetScheduler fireAndForgetService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tenantNameContainer = tenantNameContainer;
            _organizationService = organizationService;
            _cookieService = cookieService;
            _validator = validator;

            _fireAndForgetScheduler = fireAndForgetService;
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

        public async Task ResetPasswordAsync(ResetPasswordDto resetDto)
        {
            var user = await _userManager.FindByEmailAsync(resetDto.Email);
            await _userManager.ResetPasswordAsync(user, resetDto.Code, resetDto.Password);
        }

        public async Task VerifyAsync(VerifyEmailDto verifyDto)
        {
            var user = await _userManager.FindByEmailAsync(verifyDto.Email);
            await _userManager.ConfirmEmailAsync(user, verifyDto.Code);
        }

        public async Task<IEnumerable<ExternalLoginDto>> GetLoginsAsync()
        {
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);
            var hasProvider = _organizationService.HasProvider(organization, AuthenticationConstants.InternalLoginProvider);
            return GetLogins(hasProvider);
        }

        public async Task SetSignInCookieAsync()
        {
            await _cookieService.SetExternalCookieAsync();
        }

        public async Task SendPasswordResetEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            _fireAndForgetScheduler.EnqueueJob<IInternalProviderNotificationService>(async notifier => await notifier.SendResetPasswordEmailAsync(user, token));
        }

        private static IList<ExternalLoginDto> GetLogins(bool hasProvider)
        {
            return hasProvider ?
                new List<ExternalLoginDto>
                {
                    new ExternalLoginDto
                    {
                        Name = AuthenticationConstants.InternalLoginProvider,
                    }
                } :
                new List<ExternalLoginDto>();
        }

        private async Task RegisterNewUserAsync(RegisterDto registerDto)
        {
            var newUser = await RegisterUserAsync(registerDto);
            await AddInternalLoginAsync(newUser);
            await AddNewUserRolesAsync(newUser);
            await SendVerificationEmailAsync(newUser);
        }

        private async Task<ApplicationUser> RegisterUserAsync(RegisterDto registerDto)
        {
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);

            var newUser = CreateNewApplicationUser(registerDto, organization);
            await _userManager.CreateAsync(newUser, registerDto.Password);

            return newUser;
        }

        private async Task RestoreUserAsync(RegisterDto registerDto)
        {
            var user = await _userManager.RestoreSoftDeletedUserByEmailAsync(registerDto.Email);

            await AddNewUserRolesAsync(user);
        }

        private async Task AddNewUserRolesAsync(ApplicationUser user)
        {
            await _roleManager.AddToRoleAsync(user, Contracts.Constants.Roles.NewUser);
            await _roleManager.AddToRoleAsync(user, Contracts.Constants.Roles.FirstLogin);
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

            var hasExternalLogin = await _userManager.HasExternalLoginAsync(user);

            _validator.CheckIfDoesNotContainValidLogin(user, hasExternalLogin);

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, registerDto.Password);

            await SendVerificationEmailAsync(user);
        }

        private async Task SendVerificationEmailAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            _fireAndForgetScheduler.EnqueueJob<IInternalProviderNotificationService>(async notifier => await notifier.SendUserVerificationEmailAsync(user, token));
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
