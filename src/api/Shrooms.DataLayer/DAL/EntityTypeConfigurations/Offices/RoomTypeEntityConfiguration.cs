using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Offices
{
    public class RoomTypeEntityConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.AddDefaultBaseModelConfiguration(true);
            builder.AddOrganization();

            builder.Property(typeof(bool?), "IsDeleted");

            builder.Property(model => model.Color)
                .HasMaxLength(7);

            builder.Property(model => model.IsWorkingRoom)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasMany(model => model.Rooms)
                .WithOne(model => model.RoomType)
                .HasForeignKey(model => model.RoomTypeId);
        }
    }
}
