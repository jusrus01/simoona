using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class PictureEntityConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(model => model.Id);
            builder.AddDefaultBaseModelConfiguration(true);
            builder.AddSoftDelete(true);
            builder.AddOrganization();
        }
    }
}
