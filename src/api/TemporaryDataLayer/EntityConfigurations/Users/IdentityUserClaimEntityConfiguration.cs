using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations.Users
{
    public class IdentityUserClaimEntityConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            builder.HasKey(model => model.Id)
                .ForSqlServerIsClustered(true)
                .HasName("PK_dbo.AspNetUserClaims");

            builder.HasIndex(model => model.UserId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_UserId");
        }
    }
}
 