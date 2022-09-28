using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Badges;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Badges
{
    public class BadgeTypeEntityConfiguration : IEntityTypeConfiguration<BadgeType>
    {
        public void Configure(EntityTypeBuilder<BadgeType> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
        }
    }
}
