using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.EntityModels.Models.Projects;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class ProjectApplicationUserEntityConfiguration : IEntityTypeConfiguration<ProjectApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ProjectApplicationUser> builder)
        {
            builder.ToTable("ProjectApplicationUsers");

            builder.Property(model => model.ProjectId)
                .HasColumnName("Project_Id");

            builder.Property(model => model.ApplicationUserId)
                .HasColumnName("ApplicationUser_Id");

            builder.HasKey(model => new { model.ProjectId, model.ApplicationUserId })
                .HasName("PK_dbo.ProjectApplicationUsers");

            builder.HasOne(model => model.Project)
                .WithMany(model => model.ProjectApplicationUsers)
                .HasForeignKey(model => model.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.Project2ApplicationUser_dbo.Projects_Project2_Id")
                .IsRequired();

            builder.HasOne(model => model.ApplicationUser)
                .WithMany(model => model.ProjectApplicationUsers)
                .HasForeignKey(model => model.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.Project2ApplicationUser_dbo.AspNetUsers_ApplicationUser_Id")
                .IsRequired();

            builder.HasIndex(model => model.ApplicationUserId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_ApplicationUser_Id");

            builder.HasIndex(model => model.ProjectId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Project_Id");
        }
    }
}
