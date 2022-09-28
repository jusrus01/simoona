using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.ServiceRequests;

namespace Shrooms.DataLayer.EntityTypeConfigurations.ServiceRequests
{
    public class ServiceRequestCommentEntityConfiguration : IEntityTypeConfiguration<ServiceRequestComment>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestComment> builder)
        {
            builder.AddSoftDelete();
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration();

            builder.HasOne(model => model.Employee)
                .WithMany()
                .HasForeignKey(model => model.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.ServiceRequestComments_dbo.AspNetUsers_EmployeeId");
        }
    }
}
