using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.AddOrganization();

            builder.HasKey(model => model.Id);

            builder.Property(model => model.Id)
                .HasMaxLength(128);

            builder.HasIndex(model => model.Subject);

            builder.Property(model => model.Subject)
                .HasMaxLength(70)
                .IsRequired();

            builder.Property(model => model.ProtectedTicket)
                .IsRequired();

            builder.Property(model => model.IssuedUtc)
                .HasColumnType("datetime");

            builder.Property(model => model.ExpiresUtc)
                .HasColumnType("datetime");

            builder.Property(model => model.Created)
                .HasColumnType("datetime");

            builder.Property(model => model.Modified)
                .HasColumnType("datetime");

            builder.HasIndex(model => model.Subject)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Subject");
        }
    }
}
