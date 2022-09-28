using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.EntityModels.Models.Projects;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class ProjectSkillEntityConfiguration : IEntityTypeConfiguration<ProjectSkill>
    {
        public void Configure(EntityTypeBuilder<ProjectSkill> builder)
        {
            builder.ToTable("ProjectSkills");

            builder.Property(model => model.ProjectId)
                .HasColumnName("Project_Id");

            builder.Property(model => model.SkillId)
                .HasColumnName("Skill_Id");

            builder.HasKey(model => new { model.ProjectId, model.SkillId })
                .HasName("PK_dbo.ProjectSkills"); // TODO: fix Class1

            builder.HasOne(model => model.Project)
                .WithMany(model => model.ProjectSkills)
                .HasForeignKey(model => model.ProjectId)
                .HasConstraintName("FK_dbo.Project2Skill_dbo.Projects_Project2_Id")
                .IsRequired();

            builder.HasOne(model => model.Skill)
                .WithMany(model => model.ProjectSkills)
                .HasForeignKey(model => model.SkillId)
                .HasConstraintName("FK_dbo.Project2Skill_dbo.Skills_Skill_Id")
                .IsRequired();

            builder.HasIndex(model => model.SkillId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Skill_Id");

            builder.HasIndex(model => model.ProjectId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Project_Id");
        }
    }
}
