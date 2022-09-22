using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class JobPositionEntityConfiguration : IEntityTypeConfiguration<JobPosition>
    {
        public void Configure(EntityTypeBuilder<JobPosition> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddOrganization();
            builder.AddSoftDelete();
        }
    }
}
