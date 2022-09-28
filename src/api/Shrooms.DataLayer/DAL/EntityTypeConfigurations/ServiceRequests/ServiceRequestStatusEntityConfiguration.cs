using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.ServiceRequests;

namespace Shrooms.DataLayer.EntityTypeConfigurations.ServiceRequests
{
    public class ServiceRequestStatusEntityConfiguration : IEntityTypeConfiguration<ServiceRequestStatus>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestStatus> builder)
        {
            builder.AddDefaultBaseModelConfiguration(true);
            builder.AddSoftDelete(true);
        }
    }
}
