using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Offices
{
    public class RoomEntityConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.AddSoftDelete(true);
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration(true);

            builder.HasIndex(model => model.FloorId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_FloorId");

            builder.HasIndex(model => model.RoomTypeId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_RoomTypeId");
        }
    }
}
