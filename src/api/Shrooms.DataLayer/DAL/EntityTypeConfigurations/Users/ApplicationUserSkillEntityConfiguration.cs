using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.EntityModels.Models.Users;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Users
{
    public class ApplicationUserSkillEntityConfiguration : IEntityTypeConfiguration<ApplicationUserSkill>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserSkill> builder)
        {
            builder.ToTable("ApplicationUserSkills");

            builder.HasKey(model => new { model.ApplicationUserId, model.SkillId })
                .HasName("PK_dbo.ApplicationUserSkills");

            builder.HasOne(model => model.ApplicationUser)
                .WithMany(model => model.ApplicationUserSkills)
                .HasForeignKey(model => model.ApplicationUserId)
                .HasConstraintName("FK_dbo.ApplicationUserSkills_dbo.AspNetUsers_ApplicationUserId")
                .IsRequired();

            builder.HasOne(model => model.Skill)
                .WithMany(model => model.ApplicationUserSkills)
                .HasForeignKey(model => model.SkillId)
                .HasConstraintName("FK_dbo.ApplicationUserSkills_dbo.Skills_SkillId")
                .IsRequired();

            builder.HasIndex(model => model.SkillId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_SkillId");

            builder.HasIndex(model => model.ApplicationUserId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_ApplicationUserId");
        }
    }
}
