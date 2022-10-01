using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.DataLayer.EntityModels.Attributes;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.DataLayer.EntityModels.Models.Badges;
using Shrooms.DataLayer.EntityModels.Models.Books;
using Shrooms.DataLayer.EntityModels.Models.Committees;
using Shrooms.DataLayer.EntityModels.Models.Events;
using Shrooms.DataLayer.EntityModels.Models.Kudos;
using Shrooms.DataLayer.EntityModels.Models.Lotteries;
using Shrooms.DataLayer.EntityModels.Models.Multiwalls;
using Shrooms.DataLayer.EntityModels.Models.Notifications;
using Shrooms.DataLayer.EntityModels.Models.Projects;
using Shrooms.DataLayer.EntityModels.Models.ServiceRequests;

namespace Shrooms.DataLayer.DAL
{
    public class ShroomsDbContextDesignFactory : IDesignTimeDbContextFactory<ShroomsDbContext>
    {
        public ShroomsDbContext CreateDbContext(string[] args)
        {
            const string tempConnString = @"Data Source=LT-LIT-SC-0879\SQLEXPRESS;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Trusted_Connection=True;Connect Timeout=60; MultipleActiveResultSets=True;Database=Test;";
            //const string tempConnString = @"Data Source=LT-LIT-SC-0879\SQLEXPRESS;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Trusted_Connection=True;Connect Timeout=60; MultipleActiveResultSets=True;Database=Test;";

            var builder = new DbContextOptionsBuilder<ShroomsDbContext>();

            builder.UseSqlServer(tempConnString);

            return new ShroomsDbContext(builder.Options, null);
        }
    }

    //[DbConfigurationType(typeof(ShroomsContextConfiguration))]
    public class ShroomsDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // By default, EF Core won't use lazy load with proxy.
        // Note: could be configured badly because we have more stuff here. I guess I could still use
        // my autofac solution from TemporaryDataLayer and keep Identity on separate configuration?
        public ShroomsDbContext(
            DbContextOptions<ShroomsDbContext> options,
            IHttpContextAccessor httpContextAccessor) // Do not forget to configure this
            :
            base(options)
        {
            _httpContextAccessor = httpContextAccessor;

            ChangeTracker.LazyLoadingEnabled = false;
        }


        // Connection string will be passed via IoC
        // like so: UseSqlSever(string)...
        // leaving for reference
        //public ShroomsDbContext(string connectionStringName)
        //    : base(connectionStringName)
        //{
        //    ConnectionName = connectionStringName;
        //    Configuration.LazyLoadingEnabled = false;
        //    Configuration.ProxyCreationEnabled = false;
        //    Database.SetInitializer<ShroomsDbContext>(null);
        //}

        public virtual DbSet<Office> Offices { get; set; }

        public virtual DbSet<Floor> Floors { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<RoomType> RoomTypes { get; set; }

        public virtual DbSet<Organization> Organizations { get; set; }

        public virtual DbSet<Page> Pages { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<QualificationLevel> QualificationLevels { get; set; }

        public virtual DbSet<AbstractClassifier> Classificators { get; set; }

        public virtual DbSet<Picture> Pictures { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Certificate> Certificates { get; set; }

        public virtual DbSet<Skill> Skills { get; set; }

        public virtual DbSet<Exam> Exams { get; set; }

        public virtual DbSet<KudosLog> KudosLogs { get; set; }

        public virtual DbSet<KudosType> KudosTypes { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<EventType> EventTypes { get; set; }

        public virtual DbSet<EventParticipant> EventsParticipants { get; set; }

        public virtual DbSet<EventOption> EventOptions { get; set; }

        public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }

        public virtual DbSet<ServiceRequestCategory> ServiceRequestCategories { get; set; }

        public virtual DbSet<ServiceRequestPriority> ServiceRequestPriorities { get; set; }

        public virtual DbSet<ServiceRequestStatus> ServiceRequestStatuses { get; set; }

        public virtual DbSet<ServiceRequestComment> ServiceRequestComments { get; set; }

        public virtual DbSet<Committee> Committees { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<BookLog> BookLogs { get; set; }

        public virtual DbSet<BookOffice> BookOffice { get; set; }

        public virtual DbSet<SyncToken> SyncTokens { get; set; }

        public virtual DbSet<Module> Modules { get; set; }
        
        public virtual DbSet<KudosBasket> KudosBaskets { get; set; }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Wall> Walls { get; set; }

        public virtual DbSet<WallMember> WallMembers { get; set; }

        public virtual DbSet<WallModerator> WallModerators { get; set; }

        public virtual DbSet<ExternalLink> ExternalLinks { get; set; }

        public virtual DbSet<JobPosition> JobPosition { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<KudosShopItem> KudosShopItems { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<BadgeCategory> BadgeCategories { get; set; }

        public virtual DbSet<BadgeType> BadgeTypes { get; set; }

        public virtual DbSet<BadgeCategoryKudosType> BadgeCategoryKudosType { get; set; }

        public virtual DbSet<BadgeLog> BadgeLogs { get; set; }

        public virtual DbSet<Lottery> Lotteries { get; set; }

        public virtual DbSet<LotteryParticipant> LotteryParticipants { get; set; }

        public virtual DbSet<VacationPage> VacationPages { get; set; }

        public virtual DbSet<FilterPreset> FilterPresets { get; set; }

        public virtual DbSet<BlacklistUser> BlacklistUsers { get; set; }

        public string ConnectionName { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TODO: Refactor
            ApplyCustomNamingConvention(modelBuilder); // TODO: figure this out after updating to EF Core 6

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public int SaveChanges(string userId)
        {
            UpdateEntityMetadata(ChangeTracker.Entries(), userId);

            new SoftDeleteHandler().Execute(ChangeTracker.Entries(), this);

            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(string userId)
        {
            UpdateEntityMetadata(ChangeTracker.Entries(), userId);

            await new SoftDeleteHandler().ExecuteAsync(ChangeTracker.Entries(), this);

            return await base.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(bool useMetaTracking = true)
        {
            if (useMetaTracking)
            {
                UpdateEntityMetadata(ChangeTracker.Entries());
            }

            await new SoftDeleteHandler().ExecuteAsync(ChangeTracker.Entries(), this);

            return await base.SaveChangesAsync();
        }

        public new int SaveChanges(bool useMetaTracking = true)
        {
            if (useMetaTracking)
            {
                UpdateEntityMetadata(ChangeTracker.Entries());
            }

            new SoftDeleteHandler().Execute(ChangeTracker.Entries(), this);

            return base.SaveChanges();
        }

        private void UpdateEntityMetadata(IEnumerable<EntityEntry> entries, string userId = "")
        {
            // Guessed _httpContextAccessor.HttpContext.Request != null, could be wrong
            if (string.IsNullOrEmpty(userId) && _httpContextAccessor.HttpContext.Request != null && _httpContextAccessor.HttpContext.User != null)
            {
                //userId = HttpContext.Current.User.Identity.GetUserId();
                userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            var now = DateTime.UtcNow;
            var items = entries
                .Where(p => p.Entity is ITrackable && p.Entity is ISoftDelete)
                .Select(x => new
                {
                    x.State,
                    Entity = x.Entity as ITrackable
                });

            foreach (var item in items)
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.Created = now;
                    item.Entity.Modified = now;
                    item.Entity.CreatedBy = userId;
                }
                else if (item.State == EntityState.Deleted || item.State == EntityState.Modified)
                {
                    item.Entity.Modified = now;
                    item.Entity.ModifiedBy = userId;
                }
            }
        }

        private void ApplyCustomNamingConvention(ModelBuilder builder)
        {
            foreach (var mutableEntityType in builder.Model.GetEntityTypes())
            {
                ApplyCustomForeignKeyNamingConvention(mutableEntityType);
                ApplyCustomPrimaryKeyNamingConvention(mutableEntityType);
                ApplyCustomIndexNamingConvetion(mutableEntityType);
            }
        }

        private void ApplyCustomIndexNamingConvetion(IMutableEntityType mutableEntityType)
        {
            var properties = mutableEntityType.GetProperties();

            foreach (var property in properties)
            {
                var index = mutableEntityType.FindIndex(property);

                if (index == null)
                {
                    continue;
                }

                index.Relational().Name = $"IX_{property.Name}";
            }
        }

        private void ApplyCustomForeignKeyNamingConvention(IMutableEntityType entityType)
        {
            // There is a better way of implementing this. Relational().ColumnName
            foreach (var property in entityType.GetProperties())
            {
                foreach (var fk in entityType.FindForeignKeys(property))
                {
                    var name = fk.Relational().Name;
                    // TODO: export these variables to a better place
                    var prefix = "_dbo.";
                    var requiredPrefixCount = 2;

                    var containsPrefix = new Regex(prefix).Matches(name).Count == requiredPrefixCount;

                    if (containsPrefix)
                    {
                        continue;
                    }

                    fk.Relational().Name = new Regex("_").Replace(name, "_dbo.", requiredPrefixCount);
                }
            }
        }

        private void ApplyCustomPrimaryKeyNamingConvention(IMutableEntityType entityType)
        {
            var pk = entityType.FindPrimaryKey();

            if (pk == null)
            {
                return;
            }

            var name = pk.Relational().Name;

            if (name.Contains("dbo"))
            {
                return;
            }

            pk.Relational().Name = pk.Relational().Name.Replace("PK_", "PK_dbo.");
        }
    }
}