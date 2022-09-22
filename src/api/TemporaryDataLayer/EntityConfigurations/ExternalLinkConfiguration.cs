using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Enums;

namespace TemporaryDataLayer
{
    internal class ExternalLinkConfiguration : IEntityTypeConfiguration<ExternalLink>
    {
        public void Configure(EntityTypeBuilder<ExternalLink> builder)
        {
            builder.AddSoftDelete();

            builder.HasKey(model => model.Id);

            builder.Property(model => model.Name)
                .IsRequired();
            builder.Property(model => model.Url)
                .IsRequired();
            builder.Property(model => model.Type)
                .IsRequired();

            builder.Property(model => model.Priority)
                .HasDefaultValue(0);

            builder.Property(model => model.Type)
                .HasDefaultValue((ExternalLinkTypeEnum)0);

            builder.Property(model => model.Created)
                .HasColumnType("datetime2");

            builder.Property(model => model.Modified)
                .HasColumnType("datetime2");

            builder.AddOrganization();
        }
    }
}
