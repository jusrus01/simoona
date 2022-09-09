using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    internal class ExternalLinkConfig : IEntityTypeConfiguration<ExternalLink>
    {
        public void Configure(EntityTypeBuilder<ExternalLink> builder)
        {
            builder.AddSoftDelete();

            builder.Property(model => model.Name)
                .IsRequired();
            builder.Property(model => model.Url)
                .IsRequired();
            builder.Property(model => model.Type)
                .IsRequired();

            builder.Property(model => model.Created)
                .HasColumnType("datetime2");

            builder.Property(model => model.Modified)
                .HasColumnType("datetime2");

            builder.HasOne(model => model.Organization)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(model => model.OrganizationId)
                .IsRequired();
        }
    }
}
