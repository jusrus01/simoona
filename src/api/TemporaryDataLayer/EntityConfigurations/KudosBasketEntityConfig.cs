using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class KudosBasketEntityConfig : IEntityTypeConfiguration<KudosBasket>
    {
        public void Configure(EntityTypeBuilder<KudosBasket> builder)
        {
            builder.AddSoftDelete();
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration();

            builder.Property(model => model.Title)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(model => model.IsActive)
                .HasDefaultValue(false);
        }
    }
}
