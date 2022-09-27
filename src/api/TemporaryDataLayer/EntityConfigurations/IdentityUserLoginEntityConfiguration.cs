using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class IdentityUserLoginEntityConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            // Added another foreign key to match the schema,
            // will need to update the old db model to have the key with this name
            builder.HasKey(model => new { model.LoginProvider, model.ProviderKey })
                .HasName("Temporary");

            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .HasName("PK_dbo.AspNetUserLogins");

            builder.Property(model => model.LoginProvider)
                .HasMaxLength(128);

            builder.Property(model => model.ProviderKey)
                .HasMaxLength(128);

            builder.HasIndex(model => model.UserId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_UserId");
        }
    }
}
