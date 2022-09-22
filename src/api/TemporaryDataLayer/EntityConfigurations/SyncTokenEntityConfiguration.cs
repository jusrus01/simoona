using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class SyncTokenEntityConfiguration : IEntityTypeConfiguration<SyncToken>
    {
        public void Configure(EntityTypeBuilder<SyncToken> builder)
        {
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration();
        }
    }
}
