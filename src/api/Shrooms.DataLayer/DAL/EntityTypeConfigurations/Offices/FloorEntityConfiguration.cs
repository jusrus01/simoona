using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Offices
{
    public class FloorEntityConfiguration : IEntityTypeConfiguration<Floor>
    {
        // TODO: Fix multiple picture ids in the model
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            builder.AddSoftDelete(true);
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration(true);

            builder.HasOne(model => model.Picture)
                .WithMany()
                .HasForeignKey("Picture_Id")
                .HasConstraintName("FK_dbo.Floors_dbo.Pictures_Picture_Id");

            builder.HasIndex("Picture_Id")
                .ForSqlServerIsClustered(false)
                .HasName("IX_Picture_Id");

            builder.HasIndex(model => model.OfficeId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_OfficeId");

            builder.Property(model => model.PictureId);

            builder.HasMany(model => model.Rooms)
                .WithOne(model => model.Floor);
        }
    }
}
