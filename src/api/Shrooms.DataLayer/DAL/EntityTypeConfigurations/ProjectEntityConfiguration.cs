using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Projects;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddOrganization(DeleteBehavior.Cascade, "FK_Org_Projects");

            builder.HasOne(model => model.Owner)
                .WithMany(model => model.OwnedProjects)
                .HasForeignKey(model => model.OwnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.Projects_dbo.AspNetUsers_OwnerId");

            builder.Property(model => model.WallId)
                .HasDefaultValue(null);

            builder.HasIndex(model => model.OwnerId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_OwnerId");

            builder.Property(model => model.OwnerId)
                .IsRequired();
        }
    }
}
