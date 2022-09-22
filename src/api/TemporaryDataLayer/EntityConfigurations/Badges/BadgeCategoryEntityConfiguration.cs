using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class BadgeCategoryEntityConfiguration : IEntityTypeConfiguration<BadgeCategory>
    {
        public void Configure(EntityTypeBuilder<BadgeCategory> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
        }
    }
}
