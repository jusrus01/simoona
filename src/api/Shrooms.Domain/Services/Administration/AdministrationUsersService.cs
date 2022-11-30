using Microsoft.EntityFrameworkCore;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.Models.Administration;
using Shrooms.Contracts.Infrastructure.ExcelGenerator;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.DataLayer.EntityModels.Models.Multiwalls;
using Shrooms.Domain.Services.Permissions;
using Shrooms.Infrastructure.ExcelGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Shrooms.Domain.Services.Email.AdministrationUsers;
using Shrooms.Domain.Services.Users;
using Shrooms.Contracts.Infrastructure.FireAndForget;
using Shrooms.Domain.ServiceValidators.Validators.UserAdministration;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using System.Security.Claims;
using System.Security.Principal;
using Shrooms.Domain.Extensions;

namespace Shrooms.Domain.Services.Administration
{
    public class AdministrationUsersService : IAdministrationUsersService
    {
        private const string UsersExcelWorksheetName = "Users";

        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IPermissionService _permissionService;
        private readonly IUserAdministrationValidator _validator;

        private readonly DbSet<ApplicationUser> _usersDbSet;
        private readonly DbSet<WallMember> _wallUsersDbSet;

        private readonly IBackgroundJobScheduler _backgroundJobScheduler;

        private readonly IUnitOfWork2 _uow;

        public AdministrationUsersService(
            IUnitOfWork2 uow,
            IPermissionService permissionService,
            IApplicationUserManager userManager,
            IApplicationRoleManager roleManager,
            IUserAdministrationValidator validator,
            IBackgroundJobScheduler backgroundJobScheduler)
        {
            _uow = uow;
            _usersDbSet = uow.GetDbSet<ApplicationUser>();
            _wallUsersDbSet = uow.GetDbSet<WallMember>();

            _validator = validator;
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionService = permissionService;
            _backgroundJobScheduler = backgroundJobScheduler;
        }

        public async Task<ByteArrayContent> GetAllUsersExcelAsync(string fileName, int organizationId)
        {
            throw new NotImplementedException();
            //var users = await _usersDbSet
            //    .Include(user => user.WorkingHours)
            //    .Include(user => user.JobPosition)
            //    .Where(user => user.OrganizationId == organizationId) 
            //    .ToListAsync();

            //var excelBuilder = _excelBuilderFactory.GetBuilder();

            //excelBuilder
            //    .AddWorksheet(UsersExcelWorksheetName)
            //    .AddHeader(
            //        UserResources.FirstName,
            //        UserResources.LastName,
            //        UserResources.Birthday,
            //        UserResources.JobTitle,
            //        UserResources.PhoneNumber,
            //        UserResources.WorkingHours,
            //        UserResources.HasPicture)
            //    .AddRows(users.AsQueryable(), MapUserToExcelRow());

            //var content = new ByteArrayContent(excelBuilder.Build());

            //content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            //{
            //    FileName = $"{fileName}.xlsx"
            //};

            //return content;
        }

        public void NotifyAboutNewUser(ApplicationUser user, int orgId)
        {
            _backgroundJobScheduler.EnqueueJob<IAdministrationNotificationService>(notifier => notifier.NotifyAboutNewUserAsync(user, orgId));
        }

        public async Task SendUserPasswordResetEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //await _notificationService.SendUserResetPasswordEmailAsync(user, token, organizationName);
            throw new NotImplementedException();
        }

        public async Task ConfirmNewUserAsync(string userId, UserAndOrganizationDto userAndOrg)
        {
            var applicationUser = await _usersDbSet.FirstAsync(user => user.Id == userId);
            _validator.CheckIfEmploymentDateIsSet(applicationUser.EmploymentDate);

            var hasRole = await _roleManager.IsInRoleAsync(applicationUser, Contracts.Constants.Roles.FirstLogin);
            _validator.CheckIfUserHasFirstLoginRole(hasRole);

            await _roleManager.AddToRoleAsync(applicationUser, Contracts.Constants.Roles.User);
            await _roleManager.RemoveFromRoleAsync(applicationUser, Contracts.Constants.Roles.NewUser);

            _backgroundJobScheduler.EnqueueJob<IAdministrationNotificationService>(notifier => notifier.SendConfirmedNotificationEmailAsync(applicationUser.Email, userAndOrg));

            SetTutorialStatus(applicationUser, false);

            await SetWelcomeKudosAsync(applicationUser);

            await AddWallsToNewUser(applicationUser, userAndOrg);
            await _uow.SaveChangesAsync(userAndOrg.UserId);
        }

        public async Task<IEnumerable<AdministrationUserDto>> GetAllUsersAsync(string sortQuery, string search, FilterDto[] filterModel, string includeProperties)
        {
            throw new NotImplementedException();
            //includeProperties = $"{includeProperties}{(includeProperties != string.Empty ? "," : string.Empty)}Roles,Skills,JobPosition,Projects";

            //var applicationUsers = await _applicationUserRepository
            //    .Get(GenerateQuery(search), orderBy: sortQuery.Contains(Contracts.Constants.Roles.NewUser) ? string.Empty : sortQuery, includeProperties: includeProperties)
            //    .ToListAsync();

            //var administrationUsers = _mapper.Map<IList<ApplicationUser>, IList<AdministrationUserDto>>(applicationUsers);

            //await SetNewUsersValuesAsync(administrationUsers, applicationUsers);

            //if (filterModel != null)
            //{
            //    administrationUsers = FilterResults(filterModel, administrationUsers);
            //}

            //if (sortQuery.StartsWith(Contracts.Constants.Roles.NewUser))
            //{
            //    administrationUsers = (sortQuery.EndsWith("asc") ? administrationUsers.OrderBy(u => u.IsNewUser) : administrationUsers.OrderByDescending(u => u.IsNewUser)).ToList();
            //}

            //return administrationUsers;
        }

        public async Task SetUserTutorialStatusToCompleteAsync(string userId)
        {
            var applicationUser = await _usersDbSet.FirstAsync(user => user.Id == userId);
            SetTutorialStatus(applicationUser, true);
            await _uow.SaveChangesAsync(userId);
        }

        public async Task<bool> GetUserTutorialStatusAsync(string userId)
        {
            return (await _usersDbSet.FirstAsync(user => user.Id == userId)).IsTutorialComplete;
        }
        
        private static Expression<Func<ApplicationUser, IExcelRow>> MapUserToExcelRow()
        {
            return user => new ExcelRow
            {
                new ExcelColumn
                {
                    Value = user.FirstName
                },

                new ExcelColumn
                {
                    Value = user.LastName
                },

                new ExcelColumn
                {
                    Value = user.BirthDay,
                    Format = ExcelWorksheetBuilderConstants.DateWithoutTimeFormat
                },

                new ExcelColumn
                {
                    Value = user.JobPosition == null ? string.Empty : user.JobPosition.Title
                },

                new ExcelColumn
                {
                    Value = StringToLong(user.PhoneNumber),
                    Format = ExcelWorksheetBuilderConstants.NumberFormat
                },

                new ExcelColumn
                {
                    Value = user.WorkingHours != null ? $"{user.WorkingHours.StartTime}-{user.WorkingHours.EndTime}" : string.Empty,
                    SetHorizontalTextCenter = true
                },

                new ExcelColumn
                {
                    Value = user.PictureId != null ? Resources.Common.Yes : Resources.Common.No,
                    SetHorizontalTextCenter = true
                }
            };
        }


        public async Task<LoggedInUserInfoDto> GetUserInfoAsync(IIdentity identity)
        {
            _validator.CheckIfIsValidClaimsIdentity(identity);
            
            var claimsIdentity = identity as ClaimsIdentity;
            var userId = claimsIdentity.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var permissions = await _permissionService.GetUserPermissionsAsync(userId);

            return CreateUserInfo(claimsIdentity, user, roles, permissions.ToList());
        }

        private static LoggedInUserInfoDto CreateUserInfo(
            ClaimsIdentity claimsIdentity,
            ApplicationUser user,
            IList<string> roles,
            IEnumerable<string> permissions)
        {
            return new LoggedInUserInfoDto
            {
                HasRegistered = true,
                Roles = roles,
                UserName = user.UserName,
                UserId = user.Id,
                OrganizationName = claimsIdentity.GetOrganizationName(),
                OrganizationId = claimsIdentity.GetOrganizationId(),
                FullName = user.FullName,
                Permissions = permissions,
                Impersonated = false, // Impersonation logic does not exist yet
                CultureCode = user.CultureCode,
                TimeZone = user.TimeZone,
                PictureId = user.PictureId
            };
        }

        private async Task SetWelcomeKudosAsync(ApplicationUser applicationUser)
        {
            //var welcomeKudosDto = await _kudosService.GetWelcomeKudosAsync();

            //if (welcomeKudosDto.WelcomeKudosAmount <= 0)
            //{
            //    return;
            //}

            //var welcomeKudos = new KudosLog
            //{
            //    EmployeeId = applicationUser.Id,
            //    OrganizationId = applicationUser.OrganizationId,
            //    Comments = welcomeKudosDto.WelcomeKudosComment,
            //    Points = welcomeKudosDto.WelcomeKudosAmount,
            //    Created = DateTime.UtcNow,
            //    Modified = DateTime.UtcNow,
            //    Status = KudosStatus.Pending,
            //    MultiplyBy = 1,
            //    KudosSystemType = KudosTypeEnum.Welcome,
            //    KudosTypeValue = Convert.ToDecimal(KudosTypeEnum.Welcome),
            //    KudosTypeName = KudosTypeEnum.Welcome.ToString()
            //};

            //_uow.GetDbSet<KudosLog>().Add(welcomeKudos);
        }

        private static Expression<Func<ApplicationUser, bool>> GenerateQuery(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            var searchKeyWords = s.Split(BusinessLayerConstants.SearchSplitter).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            return e => searchKeyWords.Count(n =>
                e.UserName.Contains(n) ||
                e.FirstName.Contains(n) ||
                e.LastName.Contains(n) ||
                e.JobPosition.Title.Contains(n) ||
                e.Skills.Select(skill => skill.Title).Contains(n) ||
                e.Projects.Any(p => p.Name.Contains(n)) ||
                e.QualificationLevel.Name.Contains(n) ||
                e.Room.Name.Contains(n) ||
                e.Room.Number.Contains(n) ||
                e.Room.RoomType.Name.Contains(n) ||
                e.Room.Floor.Name.Contains(n) ||
                e.Room.Floor.Office.Name.Contains(n)) == searchKeyWords.Count();
        }

        private static List<object> UserToUserRow(ApplicationUser user)
        {
            var result = new List<object>
            {
                user.FirstName,
                user.LastName,
                user.BirthDay,
                user.JobPosition == null ? string.Empty : user.JobPosition.Title,
                StringToLong(user.PhoneNumber),
                user.WorkingHours != null
                    ? $"{user.WorkingHours.StartTime}-{user.WorkingHours.EndTime}"
                    : string.Empty,
                user.PictureId != null ? Resources.Common.Yes : Resources.Common.No
            };

            return result;
        }

        private static long? StringToLong(string str)
        {
            var regex = new Regex(@"\D");

            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            long? number = null;
            var numbers = regex.Replace(str, string.Empty);
            if (!string.IsNullOrEmpty(numbers))
            {
                number = Convert.ToInt64(numbers);
            }

            return number;
        }

        private static void SetTutorialStatus(ApplicationUser user, bool tutorialStatus)
        {
            user.IsTutorialComplete = tutorialStatus;
        }

        private async Task SetNewUsersValuesAsync(IList<AdministrationUserDto> administrationUserDto, IEnumerable<ApplicationUser> applicationUsers)
        {
            throw new NotImplementedException();
            //var newUserRole = await _rolesRepository.Get(x => x.Name == Contracts.Constants.Roles.NewUser).Select(x => x.Id).FirstOrDefaultAsync();

            //var usersWaitingForConfirmationIds =
            //    applicationUsers.Where(x => x.Roles.Any(y => y.RoleId == newUserRole)).Select(x => x.Id).ToList();

            //foreach (var user in usersWaitingForConfirmationIds)
            //{
            //    administrationUserDto.First(x => x.Id == user).IsNewUser = true;
            //}
        }

        private static IList<AdministrationUserDto> FilterResults(IEnumerable<FilterDto> filterModel, IList<AdministrationUserDto> administrationUsers)
        {
            IEnumerable<AdministrationUserDto> filteredUsers = null;

            foreach (var filterViewModel in filterModel)
            {
                switch (filterViewModel.Key.ToLowerInvariant())
                {
                    case "jobtitle":
                        filteredUsers = administrationUsers
                            .Where(e =>
                                filterViewModel.Values.Any(v =>
                                    !string.IsNullOrWhiteSpace(e.JobTitle)
                                    && e.JobTitle.IndexOf(v, StringComparison.OrdinalIgnoreCase) >= 0));
                        break;

                    case "projects":
                        filteredUsers = administrationUsers
                            .Where(e =>
                                filterViewModel.Values.Any(v =>
                                    e.Projects != null
                                    && e.Projects.Any(p =>
                                        !string.IsNullOrWhiteSpace(p.Name)
                                        && (p.Name.IndexOf(v, StringComparison.OrdinalIgnoreCase) >= 0))));
                        break;

                    case "skills":
                        filteredUsers = administrationUsers
                            .Where(e =>
                                filterViewModel.Values.Any(v =>
                                    e.Skills != null
                                    && e.Skills.Any(s =>
                                        !string.IsNullOrWhiteSpace(s.Title)
                                        && s.Title.IndexOf(v, StringComparison.OrdinalIgnoreCase) >= 0)));
                        break;
                }
            }

            return filteredUsers?.ToList() ?? administrationUsers;
        }

        private async Task<IList<DataLayer.EntityModels.Models.Multiwalls.Wall>> GetWallsToAddForNewUserAsync(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
            //return await _wallsDbSet
            //    .Include(wall => wall.Members)
            //    .Where(wall => wall.OrganizationId == applicationUser.OrganizationId &&
            //        (wall.Type == WallType.Main || (wall.AddForNewUsers && wall.Type == WallType.UserCreated)) &&
            //        wall.Members.All(m => m.UserId != applicationUser.Id))
            //    .ToListAsync();
        }

        private async Task AddWallsToNewUser(ApplicationUser applicationUser, UserAndOrganizationDto userOrg)
        {
            var walls = await GetWallsToAddForNewUserAsync(applicationUser);

            if (!walls.Any())
            {
                return;
            }

            var timestamp = DateTime.UtcNow;

            foreach (var wall in walls)
            {
                var wallMember = new WallMember
                {
                    UserId = applicationUser.Id,
                    Wall = wall,
                    CreatedBy = userOrg.UserId,
                    ModifiedBy = userOrg.UserId,
                    Created = timestamp,
                    Modified = timestamp,
                    AppNotificationsEnabled = true,
                    EmailNotificationsEnabled = true
                };

                _wallUsersDbSet.Add(wallMember);
            }
        }
    }
}
