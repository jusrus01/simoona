using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations.ServiceRequests
{
    public class ServiceRequestPriorityEntityConfiguration : IEntityTypeConfiguration<ServiceRequestPriority>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestPriority> builder)
        {
            builder.AddDefaultBaseModelConfiguration(true);
            builder.AddSoftDelete();
        }
    }
}
