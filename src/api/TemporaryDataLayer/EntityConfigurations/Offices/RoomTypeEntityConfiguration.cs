using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations.Offices
{
    public class RoomTypeEntityConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.AddDefaultBaseModelConfiguration(true);
            builder.AddOrganization();

            builder.AddSoftDelete(true); // TODO: figure out what to do about that constraint

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
