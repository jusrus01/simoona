using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class WallEntityConfiguration : IEntityTypeConfiguration<Wall>
    {
        public void Configure(EntityTypeBuilder<Wall> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.MapRequiredOrganization();
            builder.AddSoftDelete();
        }
    }
}
