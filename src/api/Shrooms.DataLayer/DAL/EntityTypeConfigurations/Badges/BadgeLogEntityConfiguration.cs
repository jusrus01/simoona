using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Badges;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Badges
{
    public class BadgeLogEntityConfiguration : IEntityTypeConfiguration<BadgeLog>
    {
        public void Configure(EntityTypeBuilder<BadgeLog> builder)
        {
            builder.AddDefaultBaseModelConfiguration();

            builder.HasOne(model => model.Employee)
                .WithMany()
                .HasForeignKey(model => model.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.BadgeLogs_dbo.AspNetUsers_EmployeeId");
        }
    }
}
