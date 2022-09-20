using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class BadgeTypeEntityConfiguration : IEntityTypeConfiguration<BadgeType>
    {
        public void Configure(EntityTypeBuilder<BadgeType> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
        }
    }
}
