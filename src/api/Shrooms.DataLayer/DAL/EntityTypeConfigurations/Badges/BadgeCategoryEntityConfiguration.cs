using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.EntityModels.Models.Badges;
using Shrooms.DataLayer.DAL;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Badges
{
    public class BadgeCategoryEntityConfiguration : IEntityTypeConfiguration<BadgeCategory>
    {
        public void Configure(EntityTypeBuilder<BadgeCategory> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
        }
    }
}
