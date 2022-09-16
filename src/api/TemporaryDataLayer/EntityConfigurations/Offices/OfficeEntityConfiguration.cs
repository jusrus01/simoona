using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    internal class OfficeEntityConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.AddDefaultBaseModelConfiguration(true);
            builder.AddSoftDelete(true);
            builder.MapRequiredOrganization();
        }
    }
}
