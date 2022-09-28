using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.Enums;
using Shrooms.DataLayer.EntityModels.ModelsCore.Badges;
using Shrooms.DataLayer.EntityModels.ModelsCore.Books;
using Shrooms.DataLayer.EntityModels.ModelsCore.Committees;
using Shrooms.DataLayer.EntityModels.ModelsCore.Events;
using Shrooms.DataLayer.EntityModels.ModelsCore.Kudos;
using Shrooms.DataLayer.EntityModels.ModelsCore.Multiwalls;
using Shrooms.DataLayer.EntityModels.ModelsCore.Notifications;
using Shrooms.DataLayer.EntityModels.ModelsCore.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class ApplicationUser : IdentityUser, ISoftDelete, ITrackable, IOrganization
    {
        public const int MaxPasswordLength = 100;

        public const int MinPasswordLength = 6;

        public ApplicationUser()
        {
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Bio { get; set; }

        public DateTime? EmploymentDate { get; set; }

        public DateTime? BirthDay { get; set; }

        public int? WorkingHoursId { get; set; }

        public virtual WorkingHours WorkingHours { get; set; }

        public bool IsAbsent { get; set; }

        public bool IsAnonymized { get; set; }

        public string AbsentComment { get; set; }

        public decimal TotalKudos { get; set; }

        public decimal RemainingKudos { get; set; }

        public int SittingPlacesChanged { get; set; }

        public decimal SpentKudos { get; set; }

        public int? RoomId { get; set; }

        public virtual Room Room { get; set; }

        public string PictureId { get; set; }

        public int? QualificationLevelId { get; set; }

        public virtual QualificationLevel QualificationLevel { get; set; }

        public string ManagerId { get; set; }

        public virtual ApplicationUser Manager { get; set; }

        public virtual ICollection<ApplicationUser> ManagedUsers { get; set; }

        public IEnumerable<Committee> Committees 
        {
            get => CommitteesUserMembership.Select(model => model.Committee);
        }

        public IEnumerable<Committee> LeadingCommittees
        {
            get => CommitteesUserLeadership.Select(model => model.Committee);
        }

        public IEnumerable<Committee> DelegatingCommittees
        {
            get => CommitteesUserDelegates.Select(model => model.Committee); 
        }

        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        public bool IsOwner { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        public string ModifiedBy { get; set; }

        public virtual IEnumerable<Exam> Exams 
        {
            get => ApplicationUserExams.Select(model => model.Exam);
        }

        public virtual ICollection<Certificate> Certificates { get; set; }

        public virtual IEnumerable<Skill> Skills 
        { 
            get => ApplicationUserSkills.Select(model => model.Skill); 
        }

        public virtual ICollection<Book> Books { get; set; }

        public virtual ICollection<BookLog> BookLogs { get; set; }

        public virtual ICollection<BadgeLog> BadgeLogs { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<WallMember> WallUsers { get; set; }

        public virtual IEnumerable<ServiceRequestCategory> ServiceRequestCategoriesAssigned
        { 
            get => ServiceRequestCategoryApplicationUsers.Select(model => model.ServiceRequestCategory); 
        }

        public double? VacationTotalTime { get; set; }

        public double? VacationUsedTime { get; set; }

        public double? VacationUnusedTime { get; set; }

        public DateTime? VacationLastTimeUpdated { get; set; }

        public TimeSpan? DailyMailingHour { get; set; }

        public bool IsManagingDirector { get; set; }

        public string CultureCode { get; set; }

        public IEnumerable<Project> Projects 
        { 
            get => ProjectApplicationUsers.Select(model => model.Project);
        }

        public ICollection<Project> OwnedProjects { get; set; }

        public int? JobPositionId { get; set; }

        public virtual JobPosition JobPosition { get; set; }

        public virtual ICollection<BlacklistUser> BlacklistEntries { get; set; }

        public bool UserWasPreviouslyBlacklisted
        {
            get
            {
                return BlacklistEntries != null && BlacklistEntries.Any(entry => entry.Status != BlacklistStatus.Active);
            }
        }

        public string TimeZone { get; set; }

        public virtual ICollection<NotificationUser> NotificationUsers { get; set; }

        public bool IsTutorialComplete { get; set; }

        public virtual NotificationsSettings NotificationsSettings { get; set; }

        public string GoogleEmail { get; set; }

        public string FacebookEmail { get; set; }

        public int YearsEmployed
        {
            get
            {
                var now = DateTime.UtcNow;
                //EmploymentDate ??= now;
                if (EmploymentDate == null)
                {
                    EmploymentDate = now;
                }

                var employmentYears = now.Year - EmploymentDate.Value.Year;
                if (now < EmploymentDate.Value.AddYears(employmentYears))
                {
                    employmentYears = employmentYears >= 1 ? employmentYears - 1 : employmentYears;
                }

                return employmentYears;
            }
        }

        public void ReceiveKudos(KudosLog log)
        {
            RemainingKudos += log.Points;
            TotalKudos += log.Points;
            Modified = DateTime.UtcNow;
        }

        // Required for many-to-many
        internal ICollection<ServiceRequestCategoryApplicationUser> ServiceRequestCategoryApplicationUsers { get; set; }

        internal ICollection<ProjectApplicationUser> ProjectApplicationUsers { get; set; }

        internal ICollection<ApplicationUserExam> ApplicationUserExams { get; set; }
        
        internal ICollection<ApplicationUserSkill> ApplicationUserSkills { get; set; }

        internal ICollection<CommitteesUserMembership> CommitteesUserMembership { get; set; }

        internal ICollection<CommitteesUserLeadership> CommitteesUserLeadership { get; set; }

        internal ICollection<CommitteesUserDelegates> CommitteesUserDelegates { get; set; }
    }
}
