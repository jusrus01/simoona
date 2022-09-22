using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations.ServiceRequests
{
    public class ServiceRequestEntityConfiguration : IEntityTypeConfiguration<ServiceRequest>
    {
        public void Configure(EntityTypeBuilder<ServiceRequest> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete()
                .HasDefaultValue(null);

            builder.MapRequiredOrganization();

            builder.Property(model => model.PriorityId)
                .HasDefaultValue(0);

            builder.Property(model => model.StatusId)
                .HasDefaultValue(0);

            builder.Property(model => model.KudosAmmount)
                .HasDefaultValue(0);

            builder.HasOne(model => model.Employee)
                .WithMany()
                .HasForeignKey(model => model.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.ServiceRequests_dbo.AspNetUsers_EmployeeId");

            builder.HasOne(model => model.Priority)
                .WithMany()
                .HasForeignKey(model => model.PriorityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
