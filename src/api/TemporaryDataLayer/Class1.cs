using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TemporaryDataLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TemporaryDataLayer // TODO: remove after EF Core migration :)
{
    public class TempShroomsDbContext : IdentityDbContext<ApplicationUser> // Wrong
    {
        public TempShroomsDbContext(DbContextOptions<TempShroomsDbContext> options)
            :
            base(options)
        {
        }

        public new DbSet<ApplicationUser> Users { get; set; }

        public DbSet<ModuleOrganization> ModuleOrganizations { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<FilterPreset> FilterPresets { get; set; }

        public DbSet<ExternalLink> ExternalLinks { get; set; }

        public DbSet<WorkingHours> WorkingHours { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<BookLog> BookLogs { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookOffice> BookOffices { get; set; }

        public DbSet<VacationPage> VacationPages { get; set; }

        public DbSet<Lottery> Lotteries { get; set; }

        public DbSet<LotteryParticipant> LotteryParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ApplyCustomNamingConvention(builder); // TODO: figure this out after updating to EF Core 6

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Ignoring base models
            //builder.Ignore<BaseModel>(); // Not sure that works :)

            // Temporary WorkingHoursIgnore


            base.OnModelCreating(builder);
        }

        private void ApplyCustomNamingConvention(ModelBuilder builder)
        {
            foreach (var mutableEntityType in builder.Model.GetEntityTypes())
            {
                ApplyCustomForeignKeyNamingConvention(mutableEntityType);
                ApplyCustomPrimaryKeyNamingConvention(mutableEntityType);

                // Not sure if this should be used, because when using Many-to-Many models
                // Does not work properly

                // Could override instead of replace...

                // TODO: continue changing Book

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
            foreach (var property in entityType.GetProperties())
            {
                foreach (var fk in entityType.FindForeignKeys(property))
                {
                    fk.Relational().Name = new Regex("_").Replace(fk.Relational().Name, "_dbo.", 2);
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

            pk.Relational().Name = pk.Relational().Name.Replace("PK_", "PK_dbo.");
        }
    }
    
    // Used for tools
    public class TempShroomsDesignFactory : IDesignTimeDbContextFactory<TempShroomsDbContext>
    {
        public TempShroomsDbContext CreateDbContext(string[] args)
        {
            const string tempConnString = @"Data Source=LT-LIT-SC-0879\SQLEXPRESS;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Trusted_Connection=True;Connect Timeout=60; MultipleActiveResultSets=True;Database=Test;";
            //const string tempConnString = @"Data Source=LT-LIT-SC-0879\SQLEXPRESS;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Trusted_Connection=True;Connect Timeout=60; MultipleActiveResultSets=True;Database=Test;";

            var builder = new DbContextOptionsBuilder<TempShroomsDbContext>();

            builder.UseSqlServer(tempConnString);

            return new TempShroomsDbContext(builder.Options);
        }
    }
}
