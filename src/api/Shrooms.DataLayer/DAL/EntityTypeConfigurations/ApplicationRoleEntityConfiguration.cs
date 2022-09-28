using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class ApplicationRoleEntityConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.AddSoftDelete();
            builder.AddOrganization(foreignKeyName: "FK_dbo.AspNetRoles_dbo.Organizations_OrganizationId");

            builder.Property(model => model.Id)
                .HasMaxLength(128);

            builder.Property(model => model.Name)
                .HasMaxLength(256);

            builder.Property(model => model.CreatedTime)
                .HasColumnType("datetime")
                .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.Property(model => model.Name)
                .IsRequired();

            builder.HasKey(model => model.Id)
                .ForSqlServerIsClustered(true)
                .HasName("PK_dbo.AspNetRoles");

            builder.HasIndex(model => new { model.OrganizationId, model.Name })
                .ForSqlServerIsClustered(false)
                .HasName("IX_OrganizationId_Name");
        }
    }
}
