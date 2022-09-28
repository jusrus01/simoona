using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Kudos;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class KudosShopItemEntityConfiguration : IEntityTypeConfiguration<KudosShopItem>
    {
        public void Configure(EntityTypeBuilder<KudosShopItem> builder)
        {
            builder.AddOrganization(DeleteBehavior.Cascade);
            builder.AddSoftDelete(true);
            builder.AddDefaultBaseModelConfiguration();
        }
    }
}
