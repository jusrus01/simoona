using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.ServiceRequests;

namespace Shrooms.DataLayer.EntityTypeConfigurations.ServiceRequests
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
