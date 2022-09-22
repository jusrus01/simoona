using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class KudosShopItemEntityConfiguration : IEntityTypeConfiguration<KudosShopItem>
    {
        public void Configure(EntityTypeBuilder<KudosShopItem> builder)
        {
            builder.MapRequiredOrganization(DeleteBehavior.Cascade);
            builder.AddSoftDelete(true);
            builder.AddDefaultBaseModelConfiguration();
        }
    }
}
