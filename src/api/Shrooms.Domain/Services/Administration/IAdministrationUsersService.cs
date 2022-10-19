﻿using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.Models.Administration;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Domain.Services.Administration
{
    public interface IAdministrationUsersService
    {
        Task RegisterInternalAsync(RegisterDto registerDto);

        Task<bool> UserEmailExistsAsync(string email);

        Task<ByteArrayContent> GetAllUsersExcelAsync(string fileName, int organizationId);

        Task ConfirmNewUserAsync(string userId, UserAndOrganizationDto userAndOrg);

        Task<bool> HasExistingExternalLoginAsync(string email, string loginProvider);

        //Task<IdentityResult> CreateNewUserWithExternalLoginAsync(ExternalLoginInfo info, string requestedOrganization);

        Task<IdentityResult> CreateNewUserAsync(ApplicationUser user, string password, string requestedOrganization);

        Task<IEnumerable<AdministrationUserDto>> GetAllUsersAsync(string sortQuery, string search, FilterDto[] filter, string includeProperties);

        Task<bool> IsUserSoftDeletedAsync(string email);

        Task RestoreUserAsync(string email);

        Task AddProviderImageAsync(string userId, ClaimsIdentity externalIdentity);

        Task NotifyAboutNewUserAsync(ApplicationUser user, int orgId);

        Task SetUserTutorialStatusToCompleteAsync(string userId);

        Task<bool> GetUserTutorialStatusAsync(string userId);

        Task AddProviderEmailAsync(string userId, string provider, string email);

        Task SendUserPasswordResetEmailAsync(ApplicationUser user, string organizationName);

        Task SendUserVerificationEmailAsync(ApplicationUser user, string organizationName);

        Task<LoggedInUserInfoDto> GetUserInfoAsync(IIdentity identity);
    }
}
