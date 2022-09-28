using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class RolePermissionEntityConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");

            builder.ConfigureManyToMany(
                model => new { model.PermissionId, model.RoleId },
                "RolePermissions",
                model => model.RolePermissions,
                model => model.PermissionId,
                model => model.Permission,
                "Permission",
                "FK_dbo.RolePermissions_dbo.Permissions_PermissionId",
                model => model.RolePermissions,
                model => model.RoleId,
                model => model.Role,
                "Role",
                "FK_dbo.RolePermissions_dbo.AspNetRoles_RoleId",
                includeUnderscore: false);
        }
    }
}
