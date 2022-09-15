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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ApplyCustomNamingConvention(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);


            // Ignoring base models
            //builder.Ignore<BaseModel>(); // Not sure that works :)

            // Temporary WorkingHoursIgnore


            base.OnModelCreating(builder);
        }

        // Names might not matter
        private void ApplyCustomNamingConvention(ModelBuilder builder)
        {
            foreach (var mutableEntityType in builder.Model.GetEntityTypes())
            {
                ApplyCustomForeignKeyNamingConvention(mutableEntityType);
                ApplyCustomPrimaryKeyNamingConvention(mutableEntityType);
                //ApplyCustomIndexNamingConvetion(mutableEntityType);
            }
        }

        //private void ApplyCustomIndexNamingConvetion(IMutableEntityType mutableEntityType)
        //{
        //    var indexes = mutableEntityType.GetIndexes();
        //    foreach (var index in indexes)
        //    {
        //        index.
        //        var name = index.Relational().Name;
        //        var nameParts = name.Split('_');

        //        nameParts[1] = "";

        //        index.Relational().Name = string.Join("_", nameParts);
        //    }
        //}

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
