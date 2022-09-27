using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.AddSoftDelete(true);
            builder.AddDefaultBaseModelConfiguration();

            builder.HasOne(model => model.Module)
                .WithMany()
                .HasForeignKey(model => model.ModuleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.Permissions_dbo.ShroomsModules_ModuleId");
        }
    }
}
