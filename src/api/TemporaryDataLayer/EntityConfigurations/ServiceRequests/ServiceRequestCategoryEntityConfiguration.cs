using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations.ServiceRequests
{
    public class ServiceRequestCategoryEntityConfiguration : IEntityTypeConfiguration<ServiceRequestCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestCategory> builder)
        {
            builder.AddDefaultBaseModelConfiguration(true);
            builder.AddSoftDelete();
        }
    }
}
