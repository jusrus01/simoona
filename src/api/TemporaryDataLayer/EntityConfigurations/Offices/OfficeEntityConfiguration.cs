using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class OfficeEntityConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.AddDefaultBaseModelConfiguration(true);
            builder.AddSoftDelete(true);
            builder.AddOrganization();

            builder.OwnsOne(model => model.Address);

            builder.HasMany(model => model.BookOffices)
                .WithOne(model => model.Office)
                .HasForeignKey(model => model.OfficeId);

            builder.HasMany(model => model.Floors)
                .WithOne(model => model.Office)
                .HasForeignKey(model => model.OfficeId);
        }
    }
}
