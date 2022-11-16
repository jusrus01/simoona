using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Exceptions.Exceptions.Organization;
using Shrooms.Domain.Services.Roles;
using Shrooms.Infrastructure.FireAndForget;

// Note: Name is used in claims, ShortName is used elsewhere
namespace Shrooms.Domain.Services.Organizations
{
    public class OrganizationService : IOrganizationService
    {
        private const string ProviderSeparator = ";";

        private readonly DbSet<Organization> _organizationsDbSet;
        private readonly DbSet<ApplicationUser> _usersDbSet;
        private readonly IRoleService _roleService;
        private readonly IUnitOfWork2 _uow;

        public OrganizationService(IUnitOfWork2 uow, IRoleService roleService)
        {
            _organizationsDbSet = uow.GetDbSet<Organization>();
            _usersDbSet = uow.GetDbSet<ApplicationUser>();
            _roleService = roleService;
            _uow = uow;
        }

        public async Task<Organization> GetOrganizationByIdAsync(int id)
        {
            return await _organizationsDbSet.FindAsync(id);
        }

#pragma warning disable S1449 //EF does does not understand Equals(invariant) or ToLowerInvariant()
        public async Task<Organization> GetOrganizationByNameAsync(string organizationName)
        {
            return await _organizationsDbSet.SingleAsync(organization => 
                organization.ShortName.ToLower() == organizationName.ToLower());
        }

        public string GetOrganizationHostName(string organizationName)
        {
            var hostName = _organizationsDbSet.Single(organization => organization.ShortName.ToLower() == organizationName.ToLower()).HostName;
            return hostName;
        }

        public async Task<string> GetOrganizationHostNameAsync(string organizationName)
        {
            var hostName = (await _organizationsDbSet.SingleAsync(organization => organization.ShortName.ToLower() == organizationName.ToLower())).HostName;
            return hostName;
        }

        public bool HasOrganizationEmailDomainRestriction(string organizationName)
        {
            var organization = _organizationsDbSet
                .SingleOrDefault(o => o.ShortName.ToLower() == organizationName.ToLower());

            if (organization == null)
            {
                throw new InvalidOrganizationException();
            }

            return organization.HasRestrictedAccess;
        }

        public async Task<bool> HasOrganizationEmailDomainRestrictionAsync(string organizationName)
        {
            var organization = await _organizationsDbSet
                .SingleOrDefaultAsync(o => o.ShortName.ToLower() == organizationName.ToLower());

            if (organization == null)
            {
                throw new InvalidOrganizationException();
            }

            return organization.HasRestrictedAccess;
        }
#pragma warning restore S1449

        public async Task<Organization> GetUserOrganizationAsync(ApplicationUser user)
        {
            var foundUser = await _usersDbSet.Include(user => user.Organization)
                .SingleOrDefaultAsync(u => u.Id == user.Id);

            return foundUser.Organization;
        }

        public async Task<bool> IsOrganizationHostValidAsync(string email, string requestedOrganizationName)
        {
            if (!await HasOrganizationEmailDomainRestrictionAsync(requestedOrganizationName))
            {
                return true;
            }

            var validEmailHostName = await GetOrganizationHostNameAsync(requestedOrganizationName);
            if (GetHostFromEmail(email) != validEmailHostName)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RequiresUserConfirmationAsync(int organizationId)
        {
            return await _organizationsDbSet
                .Where(o => o.Id == organizationId)
                .Select(o => o.RequiresUserConfirmation)
                .FirstAsync();
        }

        public async Task<bool> HasOrganizationAsync(string organizationName)
        {
            return await _organizationsDbSet.AnyAsync(organization => 
                organization.ShortName == organizationName || 
                organization.Name == organizationName);
        }

        public async Task<UserDto> GetManagingDirectorAsync(int organizationId)
        {
            var managingDirector = await _usersDbSet
                .Where(x => x.OrganizationId == organizationId && x.IsManagingDirector)
                .Select(x => new UserDto
                {
                    UserId = x.Id,
                    // Don't change to string interpolation, since EF won't handle project it to SQL
                    FullName = x.FirstName + " " + x.LastName
                })
                .FirstOrDefaultAsync();

            return managingDirector;
        }

        public async Task SetManagingDirectorAsync(string userId, UserAndOrganizationDto userAndOrganizationDto)
        {
            throw new NotImplementedException();
            //if (!await _roleService.HasRoleAsync(userId, Contracts.Constants.Roles.Manager))
            //{
            //    throw new ValidationException(ErrorCodes.UserIsNotAManager, "User need to have manager role to become a managing director");
            //}

            //var managingDirectors = await _usersDbSet
            //    .Include(x => x.Roles)
            //    .Where(x => x.OrganizationId == userAndOrganizationDto.OrganizationId)
            //    .Where(x => x.IsManagingDirector || x.Id == userId)
            //    .ToListAsync();

            //foreach (var currentDirector in managingDirectors.Where(x => x.IsManagingDirector))
            //{
            //    currentDirector.IsManagingDirector = false;
            //}

            //var newManagingDirector = managingDirectors.First(x => x.Id == userId);

            //newManagingDirector.IsManagingDirector = true;

            //await _uow.SaveChangesAsync(userAndOrganizationDto.UserId);
        }

        private static string GetHostFromEmail(string email)
        {
            var mailAddress = new MailAddress(email);
            return mailAddress.Host.ToLowerInvariant();
        }

        public bool HasProvider(Organization organization, string provider)
        {
            if (string.IsNullOrEmpty(provider))
            {
                return false;
            }

            var lowerCaseProvider = provider.ToLower();

            return organization.AuthenticationProviders.Split(ProviderSeparator, StringSplitOptions.RemoveEmptyEntries)
                .Any(availableProvider => availableProvider.ToLower() == lowerCaseProvider);
        }
    }
}
