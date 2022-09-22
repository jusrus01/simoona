using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    internal class PageEntityConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.AddSoftDelete(true);
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration(true);
        }
    }
}
