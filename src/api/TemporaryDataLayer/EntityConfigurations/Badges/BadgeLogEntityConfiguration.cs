using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
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
