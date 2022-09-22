using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations.ServiceRequests
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
