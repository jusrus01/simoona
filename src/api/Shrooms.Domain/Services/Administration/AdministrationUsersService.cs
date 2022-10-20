using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.Models.Administration;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.Contracts.Exceptions;
using Shrooms.Contracts.Infrastructure.ExcelGenerator;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.DataLayer.EntityModels.Models.Multiwalls;
using System.Web;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Permissions;
using Shrooms.Infrastructure.ExcelGenerator;
using Shrooms.Infrastructure.FireAndForget;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Administration
{
    public class AdministrationUsersService : IAdministrationUsersService
    {
        private const string UsersExcelWorksheetName = "Users";
        private const int StateStrengthInBits = 256;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly IPermissionService _permissionService;

        private readonly ApplicationOptions _applicationOptions;

        private readonly AuthenticationService _authenticationService;

        //private readonly IRepository<ApplicationUser> _applicationUserRepository;
        private readonly DbSet<ApplicationUser> _usersDbSet;
        private readonly DbSet<Organization> _organizationDbSet;
        private readonly DbSet<WallMember> _wallUsersDbSet;
        private readonly IOrganizationService _organizationService;
        //private readonly IPictureService _pictureService;
        //private readonly IMapper _mapper;
        //private readonly IAdministrationNotificationService _notificationService;
        //private readonly IKudosService _kudosService;
        private readonly IUnitOfWork2 _uow;
        //private readonly IExcelBuilderFactory _excelBuilderFactory;

        public AdministrationUsersService(
            IDbContext dbContext,
            //IUnitOfWork unitOfWork,
            IUnitOfWork2 uow,
            //IUserAdministrationValidator userAdministrationValidator,
            IOrganizationService organizationService,
            //IPictureService pictureService,
            //IDbContext context,
            //IAdministrationNotificationService notificationService,
            //IKudosService kudosService,
            //IExcelBuilderFactory excelBuilderFactory,
            ITenantNameContainer tenantNameContainer,
            IPermissionService permissionService,
            UserManager<ApplicationUser> userManager,
            IAuthenticationService authenticationService,
            IOptions<ApplicationOptions> applicationOptions)
        {
            _uow = uow;
            //_mapper = mapper;
            //_applicationUserRepository = unitOfWork.GetRepository<ApplicationUser>();
            _usersDbSet = uow.GetDbSet<ApplicationUser>();
            _organizationDbSet = uow.GetDbSet<Organization>();
            _wallUsersDbSet = uow.GetDbSet<WallMember>();
            _organizationService = organizationService;
            //_notificationService = notificationService;
            //_kudosService = kudosService;

            _userManager = userManager;
            _tenantNameContainer = tenantNameContainer;
            _permissionService = permissionService;
            _authenticationService = (AuthenticationService)authenticationService;

            _applicationOptions = applicationOptions.Value;
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

        // TODO: Change logic when ShroomsDbContext is configured correctly
        public async Task<bool> IsUserSoftDeletedAsync(string email)
        {
            var softDeleteUser = await _usersDbSet.FromSql($"SELECT * FROM [dbo].[AspNetUsers] WHERE Email = {email} AND IsDeleted = 1") // Temporary
                .SingleOrDefaultAsync();

            return softDeleteUser != null;
        }

        public async Task RestoreUserAsync(string email)
        {
            throw new NotImplementedException();
            //var shroomsContext = _context as ShroomsDbContext;

            //if (shroomsContext == null)
            //{
            //    throw new ArgumentNullException(nameof(shroomsContext));
            //}

            //await shroomsContext.Database
            //    .ExecuteSqlCommandAsync("UPDATE [dbo].[AspNetUsers] SET[IsDeleted] = '0' WHERE Email = @email", new SqlParameter("@email", email));

            //var user = await _userManager.FindByEmailAsync(email);
            //await AddNewUserRolesAsync(user.Id);
        }

        public async Task AddProviderImageAsync(string userId, ClaimsIdentity externalIdentity)
        {
            throw new NotImplementedException();

            //var user = await _usersDbSet.FirstAsync(u => u.Id == userId);
            //if (user.PictureId == null && externalIdentity.FindFirst("picture") != null)
            //{
            //    byte[] data = data = await new WebClient().DownloadDataTaskAsync(externalIdentity.FindFirst("picture").Value);
            //    user.PictureId = await _pictureService.UploadFromStreamAsync(new MemoryStream(data), "image/jpeg", Guid.NewGuid() + ".jpg", user.OrganizationId);
            //    await _uow.SaveChangesAsync(userId);
            //}
        }

        public async Task NotifyAboutNewUserAsync(ApplicationUser user, int orgId)
        {
            throw new NotImplementedException();
            //await _notificationService.NotifyAboutNewUserAsync(user, orgId);
        }

        public async Task SendUserPasswordResetEmailAsync(ApplicationUser user, string organizationName)
        {
            throw new NotImplementedException();
            //var token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);

            //await _notificationService.SendUserResetPasswordEmailAsync(user, token, organizationName);
        }

        public async Task SendUserVerificationEmailAsync(ApplicationUser user, string orgazinationName)
        {
            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);

            //await _notificationService.SendUserVerificationEmailAsync(user, token, orgazinationName);
        }

        public async Task ConfirmNewUserAsync(string userId, UserAndOrganizationDto userAndOrg)
        {
            throw new NotImplementedException();
            //var applicationUser = await _usersDbSet.FirstAsync(user => user.Id == userId);
            //_userAdministrationValidator.CheckIfEmploymentDateIsSet(applicationUser.EmploymentDate);

            //var hasRole = await _userManager.IsInRoleAsync(userId, Contracts.Constants.Roles.FirstLogin);
            //_userAdministrationValidator.CheckIfUserHasFirstLoginRole(hasRole);

            //var addRoleResult = await _userManager.AddToRoleAsync(userId, Contracts.Constants.Roles.User);
            //var removeRoleResult = await _userManager.RemoveFromRoleAsync(userId, Contracts.Constants.Roles.NewUser);

            //_userAdministrationValidator.CheckForAddingRemovingRoleErrors(addRoleResult.Errors.ToList(), removeRoleResult.Errors.ToList());
            //await _notificationService.SendConfirmedNotificationEmailAsync(applicationUser.Email, userAndOrg);

            //SetTutorialStatus(applicationUser, false);

            //await SetWelcomeKudosAsync(applicationUser);

            //await AddWallsToNewUser(applicationUser, userAndOrg);
            //await _uow.SaveChangesAsync(userAndOrg.UserId);
        }

        //public async Task<IdentityResult> CreateNewUserWithExternalLoginAsync(ExternalLoginInfo info, string requestedOrganization)
        //{
        //    var externalIdentity = info.ExternalIdentity;
        //    var userSettings = await _organizationDbSet.Where(o => o.ShortName == requestedOrganization)
        //        .Select(u => new { u.CultureCode, u.TimeZone })
        //        .FirstAsync();

        //    var user = new ApplicationUser
        //    {
        //        UserName = externalIdentity.FindFirst(ClaimTypes.Email).Value,
        //        Email = externalIdentity.FindFirst(ClaimTypes.Email).Value,
        //        FirstName = externalIdentity.FindFirst(ClaimTypes.GivenName).Value,
        //        LastName = externalIdentity.FindFirst(ClaimTypes.Surname).Value,
        //        OrganizationId = (await _organizationService.GetOrganizationByNameAsync(requestedOrganization)).Id,
        //        EmploymentDate = DateTime.UtcNow,
        //        CultureCode = userSettings.CultureCode ?? BusinessLayerConstants.DefaultCulture,
        //        TimeZone = userSettings.TimeZone,
        //        NotificationsSettings = null
        //    };

        //    if (externalIdentity.FindFirst("picture") != null)
        //    {
        //        var data = await new WebClient().DownloadDataTaskAsync(externalIdentity.FindFirst("picture").Value);
        //        var picture = await _pictureService.UploadFromStreamAsync(new MemoryStream(data), "image/jpeg", $"{Guid.NewGuid()}.jpg", user.OrganizationId);
        //        user.PictureId = picture;
        //    }

        //    var result = _userManager.Create(user);
        //    if (!result.Succeeded)
        //    {
        //        return result;
        //    }

        //    await AddNewUserRolesAsync(user.Id);
        //    return result;
        //}

        public async Task<IdentityResult> CreateNewUserAsync(ApplicationUser user, string password, string requestedOrganization)
        {
            throw new NotImplementedException();
            //var userSettings = await _organizationDbSet.Where(o => o.ShortName == requestedOrganization)
            //    .Select(u => new { u.CultureCode, u.TimeZone })
            //    .FirstAsync();

            //user.OrganizationId = (await _organizationService.GetOrganizationByNameAsync(requestedOrganization)).Id;
            //user.EmploymentDate = DateTime.UtcNow;
            //user.CultureCode = userSettings.CultureCode ?? BusinessLayerConstants.DefaultCulture;
            //user.TimeZone = userSettings.TimeZone;
            //user.NotificationsSettings = null;

            //var result = await _userManager.CreateAsync(user, password);
            //if (!result.Succeeded)
            //{
            //    return result;
            //}

            //var userLoginInfo = new UserLoginInfo(AuthenticationConstants.InternalLoginProvider, user.Id);
            //var addLoginResult = await _userManager.AddLoginAsync(user.Id, userLoginInfo);
            //if (!addLoginResult.Succeeded)
            //{
            //    return addLoginResult;
            //}

            //await AddNewUserRolesAsync(user.Id);
            //await SendUserVerificationEmailAsync(user, requestedOrganization);

            //return result;
        }

        public async Task<bool> HasExistingExternalLoginAsync(string email, string loginProvider)
        {
            throw new NotImplementedException();
            //var user = await _userManager.FindByEmailAsync(email);
            //if (user == null)
            //{
            //    return false;
            //}

            //var hasLogin = user.Logins.Any(login => login.LoginProvider == loginProvider);
            //return hasLogin;
        }

        public async Task<bool> UserEmailExistsAsync(string email)
        {
            // Possible unexpected behavior when a user is registered in multiple organizations
            // when organizations are hosted on the same database instance (at the moment we keep organization's databases instances separate)
            return await _userManager.FindByEmailAsync(email) != null;
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

        public async Task AddProviderEmailAsync(string userId, string provider, string email)
        {
            var user = await _usersDbSet.FirstAsync(u => u.Id == userId);
            if (provider == AuthenticationConstants.GoogleLoginProvider)
            {
                user.GoogleEmail = email;
            }

            if (provider == AuthenticationConstants.FacebookLoginProvider)
            {
                user.FacebookEmail = email;
            }

            await _uow.SaveChangesAsync(userId);
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

        private async Task AddNewUserRolesAsync(ApplicationUser user)
        {
            await _userManager.AddToRoleAsync(user, Contracts.Constants.Roles.NewUser);
            await _userManager.AddToRoleAsync(user, Contracts.Constants.Roles.FirstLogin);
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

        public async Task<IEnumerable<ExternalLoginDto>> GetInternalLoginsAsync()
        {
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);
            var providers = organization.AuthenticationProviders;

            if (!ContainsProvider(providers, AuthenticationConstants.InternalLoginProvider.ToLower()))
            {
                return Enumerable.Empty<ExternalLoginDto>();
            }

            return new List<ExternalLoginDto>
            {
                new ExternalLoginDto
                {
                    Name = AuthenticationConstants.InternalLoginProvider,
                }
            };
        }

        public async Task<LoggedInUserInfoDto> GetUserInfoAsync(IIdentity identity)
        {
            if (identity is not ClaimsIdentity claimsIdentity)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Invalid identity");
            }

            if (!claimsIdentity.IsAuthenticated)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "User not authenticated");
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ValidationException(ErrorCodes.UserNotFound, "User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var permissions = await _permissionService.GetUserPermissionsAsync(userId);

            return new LoggedInUserInfoDto
            {
                HasRegistered = true,
                Roles = roles,
                UserName = identity.Name,
                UserId = userId,
                OrganizationName = claimsIdentity.FindFirst(WebApiConstants.ClaimOrganizationName).Value,
                OrganizationId = claimsIdentity.FindFirst(WebApiConstants.ClaimOrganizationId).Value,
                FullName = claimsIdentity.FindFirst(ClaimTypes.GivenName).Value,
                Permissions = permissions,
                Impersonated = claimsIdentity?.Claims.Any(c => c.Type == WebApiConstants.ClaimUserImpersonation && c.Value == true.ToString()) ?? false, // TODO: Change
                CultureCode = user.CultureCode,
                TimeZone = user.TimeZone,
                PictureId = user.PictureId
            };
        }

        // TODO: Register scheme only if key exists (on start up, before adding facebook signin)
        public async Task<IEnumerable<ExternalLoginDto>> GetExternalLoginsAsync(string controllerName, string returnUrl, string userId)
        {
            var externalLogins = new List<ExternalLoginDto>();

            var availableAuthenticationSchemes = await _authenticationService.Schemes.GetAllSchemesAsync();
            var organization = await _organizationService.GetOrganizationByNameAsync(_tenantNameContainer.TenantName);

            foreach (var authenticationScheme in availableAuthenticationSchemes)
            {
                if (!ContainsProvider(organization.AuthenticationProviders, authenticationScheme.Name))
                {
                    continue;
                }

                var state = GenerateExternalAuthenticationState();

                var login = new ExternalLoginDto
                {
                    Name = authenticationScheme.Name,
                    Url = CreateUrl(controllerName, authenticationScheme, returnUrl, state, userId, false),
                    State = state
                };

                externalLogins.Add(login);

                state = GenerateExternalAuthenticationState();
                login = new ExternalLoginDto
                {
                    Name = $"{authenticationScheme.Name}Registration",
                    Url = CreateUrl(controllerName, authenticationScheme, returnUrl, state, userId, true),
                    State = state
                };

                externalLogins.Add(login);
            }

            return externalLogins;
        }

        // TODO: Update registration logic when organization logic is fixed
        public async Task RegisterInternalAsync(RegisterDto registerDto)
        {
            if (!await UserEmailExistsAsync(registerDto.Email))
            {
                await HandleNormalRegistrationAsync(registerDto);
                return;
            }

            if (await IsUserSoftDeletedAsync(registerDto.Email))
            {
                await HandleSoftDeletedUserRegistrationAsync(registerDto);
                return;
            }

            await HandleExistingUserRegistrationAsync(registerDto);
        }

        private string CreateUrl(
            string controllerName,
            AuthenticationScheme authenticationScheme,
            string returnUrl,
            string state,
            string userId,
            bool isRegistration)
        {
            // TODO: Check if ExternalLogin exists? On start up?
            // TODO: Refactor
            return string.Concat(
                "/",
                controllerName,
                "/ExternalLogin?",
                $"provider={authenticationScheme.Name}&",
                $"organization={_tenantNameContainer.TenantName}&",
                $"response_type=token&",
                $"client_id={_applicationOptions.ClientId}&",
                $"redirect_url={new Uri($"{returnUrl}?authType={authenticationScheme.Name}").AbsoluteUri}&",
                $"state={state}",
                userId != null ? $"userId={userId}" : "",
                isRegistration ? "isRegistration=true" : "");
        }

        private static bool ContainsProvider(string providerList, string providerName)
        {
            return providerList.ToLower().Contains(providerName.ToLower());
        }

        private async Task HandleNormalRegistrationAsync(RegisterDto registerDto)
        {
            await CreateNewInternalUserAsync(registerDto);
        }

        private async Task HandleSoftDeletedUserRegistrationAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        private async Task HandleExistingUserRegistrationAsync(RegisterDto registerDto)
        {
            // Possible unexpected behavior when a user tries to register for another organization with the same email address
            // when organizations are hosted on the same database instance (at the moment we keep organization's databases instances separate)
            var user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user.EmailConfirmed || await IsUserRegisteredByExternalProviderAsync(user))
            {
                throw new ValidationException(ErrorCodes.DuplicatesIntolerable, "User is already registered");
            }

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, registerDto.Password);

            // TODO: Send verification email
        }

        private async Task CreateNewInternalUserAsync(RegisterDto registerDto)
        {
            var tenantName = _tenantNameContainer.TenantName;

            var defaultUserOrganizationSettings = await _organizationDbSet.Where(o => o.ShortName == tenantName)
                .Select(u => new { u.CultureCode, u.TimeZone })
                .FirstAsync();

            var organization = await _organizationService.GetOrganizationByNameAsync(tenantName);

            var newUser = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                EmploymentDate = DateTime.UtcNow,
                CultureCode = defaultUserOrganizationSettings.CultureCode ?? BusinessLayerConstants.DefaultCulture,
                TimeZone = defaultUserOrganizationSettings.TimeZone,
                NotificationsSettings = null,
                OrganizationId = organization.Id,
            };

            var createdUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createdUserResult.Succeeded)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Failed to create user");
            }

            await AddNewUserRolesAsync(newUser);
            // TODO: Send email verification
        }

        private async Task<bool> IsUserRegisteredByExternalProviderAsync(ApplicationUser user)
        {
            // Note: Internal provider is removed
            var logins = await _userManager.GetLoginsAsync(user);

            return logins.Any();
        }

        private string GenerateExternalAuthenticationState()
        {
            const int bitsPerByte = 8;

            using var cryptoProvider = new RNGCryptoServiceProvider();

            if (StateStrengthInBits % bitsPerByte != 0)
            {
                throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
            }

            var strengthInBytes = StateStrengthInBits / bitsPerByte;

            var data = new byte[strengthInBytes];

            cryptoProvider.GetBytes(data);

            //return HttpServerUtility.UrlTokenEncode(data); // TODO: Make sure that state is fine

            var base64 = Convert.ToBase64String(data);

            return base64[0..^1];
        }
    }
}
