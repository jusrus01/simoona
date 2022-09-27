using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class FilterPresetEntityConfiguration : IEntityTypeConfiguration<FilterPreset>
    {
        public void Configure(EntityTypeBuilder<FilterPreset> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete();
            builder.AddOrganization(DeleteBehavior.Cascade); // Probably should not be Cascade, however old model has it

            builder.HasKey(model => model.Id);

            builder.Property(model => model.Preset)
                .IsRequired();

            builder.Property(filter => filter.Name)
                .IsRequired();

            builder.Property(filter => filter.ForPage)
                .IsRequired();
        }
    }
}
