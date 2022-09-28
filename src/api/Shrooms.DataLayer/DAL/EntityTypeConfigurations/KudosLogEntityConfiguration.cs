using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Enums;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Kudos;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class KudosLogEntityConfiguration : IEntityTypeConfiguration<KudosLog>
    {
        public void Configure(EntityTypeBuilder<KudosLog> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddOrganization(DeleteBehavior.Cascade);

            builder.Property(model => model.Comments)
                .IsRequired();

            builder.Property(model => model.KudosSystemType)
                .HasDefaultValue(KudosTypeEnum.Ordinary);

            builder.HasOne(model => model.Employee)
                .WithMany()
                .HasForeignKey(model => model.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.KudosLogs_dbo.AspNetUsers_EmployeeId");
        }
    }
}
