using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Shrooms.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TemporaryDataLayer.EntityConfigurations;
using TemporaryDataLayer.Models;

namespace TemporaryDataLayer // TODO: remove after EF Core migration :)
{
    public class TestClass
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }
    }

    // Add-Migration Initial -Project TemporaryDataLayer -StartUpProject Shrooms.Presentation.Api
    // NOTE: this works fine (before adding Identity)
    //public class TempShroomsDbContext : DbContext
    //{
    //    public TempShroomsDbContext(DbContextOptions<TempShroomsDbContext> options)
    //        :
    //        base(options)
    //    {
    //    }

    //    public DbSet<TestClass> Tests { get; set; }
    //}

    //public class TempShroomsDesignFactory : IDesignTimeDbContextFactory<TempShroomsDbContext>
    //{
    //    public TempShroomsDbContext CreateDbContext(string[] args)
    //    {
    //        const string tempConnString = @"Data Source=LT-LIT-SC-0879\SQLEXPRESS;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Trusted_Connection=True;Connect Timeout=60; MultipleActiveResultSets=True;Database=Test;";
    //        var builder = new DbContextOptionsBuilder<TempShroomsDbContext>();

    //        builder.UseSqlServer(tempConnString);

    //        return new TempShroomsDbContext(builder.Options);
    //    }
    //}

    // After adding identity
    
    public class TempApplicationUser : IdentityUser, ISoftDelete, ITrackable
    {
        public const int MaxPasswordLength = 100;

        public const int MinPasswordLength = 6;

        public TempApplicationUser()
        {
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        public string Bio { get; set; }

        public DateTime? EmploymentDate { get; set; }

        public DateTime? BirthDay { get; set; }

        public int? WorkingHoursId { get; set; }

        public bool IsAbsent { get; set; }

        public bool IsAnonymized { get; set; }

        public string AbsentComment { get; set; }

        public decimal TotalKudos { get; set; }

        public decimal RemainingKudos { get; set; }

        public int SittingPlacesChanged { get; set; }

        public decimal SpentKudos { get; set; }

        //[ForeignKey(nameof(Room))]
        public int? RoomId { get; set; }


        public string PictureId { get; set; }

        //[ForeignKey(nameof(QualificationLevel))]
        public int? QualificationLevelId { get; set; }


        public string ManagerId { get; set; }

        //[ForeignKey(nameof(ManagerId))]

        //public virtual ICollection<ApplicationUser> ManagedUsers { get; set; }

        //public virtual ICollection<Committee.Committee> Committees { get; set; }

        //[InverseProperty(nameof(Committee.Committee.Leads))]
        //public virtual ICollection<Committee.Committee> LeadingCommittees { get; set; }

        //[InverseProperty(nameof(Committee.Committee.Delegates))]
        //public virtual ICollection<Committee.Committee> DelegatingCommittees { get; set; }

        public int OrganizationId { get; set; }


        public bool IsOwner { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        public string ModifiedBy { get; set; }

        //public virtual ICollection<Exam> Exams { get; set; }

        //public virtual ICollection<Certificate> Certificates { get; set; }

        //public virtual ICollection<Skill> Skills { get; set; }

        //public virtual ICollection<Book> Books { get; set; }

        //public virtual ICollection<BookLog> BookLogs { get; set; }

        //public virtual ICollection<BadgeLog> BadgeLogs { get; set; }

        //public virtual ICollection<Event> Events { get; set; }

        //public virtual ICollection<WallMember> WallUsers { get; set; }

        //public virtual ICollection<ServiceRequestCategory> ServiceRequestCategoriesAssigned { get; set; }

        public double? VacationTotalTime { get; set; }

        public double? VacationUsedTime { get; set; }

        public double? VacationUnusedTime { get; set; }

        public DateTime? VacationLastTimeUpdated { get; set; }

        public TimeSpan? DailyMailingHour { get; set; }

        public bool IsManagingDirector { get; set; }

        public string CultureCode { get; set; }

        //public virtual ICollection<Project> Projects { get; set; }

        //public virtual ICollection<Project> OwnedProjects { get; set; }

        public int? JobPositionId { get; set; }

        //[ForeignKey(nameof(JobPositionId))]

        //[InverseProperty(nameof(BlacklistUser.User))]
        //public virtual ICollection<BlacklistUser> BlacklistEntries { get; set; }

        //[NotMapped]
        //public bool UserWasPreviouslyBlacklisted
        //{
        //    get
        //    {
        //        return BlacklistEntries != null && BlacklistEntries.Any(entry => entry.Status != BlacklistStatus.Active);
        //    }
        //}

        public string TimeZone { get; set; }

        //public virtual ICollection<NotificationUser> NotificationUsers { get; set; }

        public bool IsTutorialComplete { get; set; }


        public string GoogleEmail { get; set; }

        public string FacebookEmail { get; set; }

        [NotMapped]
        public int YearsEmployed
        {
            get
            {
                var now = DateTime.UtcNow;
                //EmploymentDate ??= now;

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
    }
    
    public class TempShroomsDbContext : IdentityDbContext<TempApplicationUser> // Trying without ApplicationUser class
    {
        public TempShroomsDbContext(DbContextOptions<TempShroomsDbContext> options)
            :
            base(options)
        {
        }

        public DbSet<TestClass> Tests { get; set; }



        // Tables
        public DbSet<ModuleOrganization> ModuleOrganizations { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Module> Modules { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Could use reflection to use all of the available configs
            builder.ApplyConfiguration(new OrganizationEntityConfig());
            builder.ApplyConfiguration(new ModuleEntityConfig());
            builder.ApplyConfiguration(new ModuleOrganizationEntityConfig());

            base.OnModelCreating(builder);
        }
    }

    public class TempShroomsDesignFactory : IDesignTimeDbContextFactory<TempShroomsDbContext>
    {
        public TempShroomsDbContext CreateDbContext(string[] args)
        {
            const string tempConnString = @"Data Source=LT-LIT-SC-0879\SQLEXPRESS;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Trusted_Connection=True;Connect Timeout=60; MultipleActiveResultSets=True;Database=Test;";
            var builder = new DbContextOptionsBuilder<TempShroomsDbContext>();

            builder.UseSqlServer(tempConnString);

            return new TempShroomsDbContext(builder.Options);
        }
    }
}
