using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Users
{
    public class IdentityUserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasIndex(model => model.UserId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_UserId");
        }
    }
}
