using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TemporaryDataLayer.Models;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class ModuleOrganizationEntityConfiguration : IEntityTypeConfiguration<ModuleOrganization>
    {
        public void Configure(EntityTypeBuilder<ModuleOrganization> builder)
        {
            builder.Property(model => model.OrganizationId)
                .HasColumnName($"{nameof(ModuleOrganization.OrganizationId).Replace("Id", "_Id")}");

            builder.Property(model => model.ModuleId)
                .HasColumnName($"{nameof(ModuleOrganization.ModuleId).Replace("Id", "_Id")}");

            builder.HasKey(model => new { model.ModuleId, model.OrganizationId })
                .HasName("PK_dbo.ModuleOrganizations"); // TODO: fix Class1

            builder.HasOne(model => model.Module)
                .WithMany(model => model.ShroomsModuleOrganizations)
                .HasForeignKey(model => model.ModuleId)
                .HasConstraintName("FK_dbo.ShroomsModuleOrganizations_dbo.ShroomsModules_ShroomsModule_Id")
                .IsRequired();

            builder.HasOne(model => model.Organization)
                .WithMany(model => model.ShroomsModuleOrganizations)
                .HasForeignKey(model => model.OrganizationId)
                .HasConstraintName("FK_dbo.ShroomsModuleOrganizations_dbo.Organizations_Organization_Id")
                .IsRequired();

            builder.HasIndex(model => model.ModuleId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Module_Id");

            builder.HasIndex(model => model.OrganizationId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_Organization_Id");
        }
    }
}