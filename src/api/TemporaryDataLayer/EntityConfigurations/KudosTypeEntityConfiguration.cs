using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Enums;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class KudosTypeEntityConfiguration : IEntityTypeConfiguration<KudosType>
    {
        public void Configure(EntityTypeBuilder<KudosType> builder)
        {
            builder.AddSoftDelete(true);
            builder.AddDefaultBaseModelConfiguration(true);

            builder.Property(model => model.Name)
                .IsRequired();

            builder.Property(model => model.Description)
                .HasMaxLength(BusinessLayerConstants.MaxKudosDescriptionLength);

            builder.Property(model => model.Type)
                .HasDefaultValue((KudosTypeEnum)0);

            builder.Property(model => model.IsActive)
                .HasDefaultValue(true);
        }
    }
}
