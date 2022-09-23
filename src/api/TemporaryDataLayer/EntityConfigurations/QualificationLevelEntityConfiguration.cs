using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class QualificationLevelEntityConfiguration : IEntityTypeConfiguration<QualificationLevel>
    {
        public void Configure(EntityTypeBuilder<QualificationLevel> builder)
        {
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration();
        }
    }
}
