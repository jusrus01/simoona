using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Enums;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Multiwalls;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class WallEntityConfiguration : IEntityTypeConfiguration<Wall>
    {
        public void Configure(EntityTypeBuilder<Wall> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddOrganization();
            builder.AddSoftDelete();

            builder.Property(model => model.Logo)
                .HasDefaultValue("wall-default.png");

            builder.Property(model => model.Type)
                .HasDefaultValue((WallType)0);

            builder.Property(model => model.Access)
                .HasDefaultValue((WallAccess)0);

            builder.Property(model => model.IsHiddenFromAllWalls)
                .HasDefaultValue(false);

            builder.Property(model => model.AddForNewUsers)
                .HasDefaultValue(false);
        }
    }
}
