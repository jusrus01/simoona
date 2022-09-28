using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.ServiceRequests;

namespace Shrooms.DataLayer.EntityTypeConfigurations.ServiceRequests
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
