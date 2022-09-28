using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class WorkingHoursEntityConfiguration : IEntityTypeConfiguration<WorkingHours>
    {
        public void Configure(EntityTypeBuilder<WorkingHours> builder)
        {
            builder.HasKey(model => model.Id);
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete();
            builder.AddOrganization();

            builder.Property(model => model.FullTime)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(model => model.ApplicationUserId)
                .IsRequired();

            builder.HasOne(model => model.ApplicationUser)
                .WithMany()
                .HasConstraintName("FK_dbo.WorkingHours_dbo.AspNetUsers_ApplicationUserId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(model => model.ApplicationUserId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_ApplicationUserId");
        }
    }
}
