using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class FilterPresetEntityConfig : IEntityTypeConfiguration<FilterPreset>
    {
        public void Configure(EntityTypeBuilder<FilterPreset> builder)
        {
            builder.AddSoftDelete();

            builder.HasKey(model => model.Id);

            builder.HasOne(model => model.Organization)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(model => model.OrganizationId)
                .IsRequired();

            builder.Property(model => model.Preset)
                .IsRequired();

            builder.Property(filter => filter.Name)
                .IsRequired();

            builder.Property(filter => filter.ForPage)
                .IsRequired();
        }
    }
}
