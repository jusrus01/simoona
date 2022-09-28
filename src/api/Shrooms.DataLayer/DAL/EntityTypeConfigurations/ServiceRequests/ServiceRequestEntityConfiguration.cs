using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.ServiceRequests;

namespace Shrooms.DataLayer.EntityTypeConfigurations.ServiceRequests
{
    public class ServiceRequestEntityConfiguration : IEntityTypeConfiguration<ServiceRequest>
    {
        public void Configure(EntityTypeBuilder<ServiceRequest> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete()
                .HasDefaultValue(null);

            builder.AddOrganization();

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
