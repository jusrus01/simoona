using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TemporaryDataLayer.Models;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class ModuleOrganizationEntityConfig : IEntityTypeConfiguration<ModuleOrganization>
    {
        public void Configure(EntityTypeBuilder<ModuleOrganization> builder)
        {
            builder.Property(model => model.OrganizationId)
                .HasColumnName($"{nameof(ModuleOrganization.OrganizationId).Replace("Id", "_Id")}");

            builder.Property(model => model.ModuleId)
                .HasColumnName($"{nameof(ModuleOrganization.ModuleId).Replace("Id", "_Id")}");

            builder.HasKey(model => new { model.ModuleId, model.OrganizationId });

            builder.HasOne(model => model.Module)
                .WithMany(model => model.ShroomsModuleOrganizations)
                .HasForeignKey(model => model.ModuleId)
                .IsRequired();

            builder.HasOne(model => model.Organization)
                .WithMany(model => model.ShroomsModuleOrganizations)
                .HasForeignKey(model => model.OrganizationId)
                .IsRequired();

            builder.HasIndex(model => model.ModuleId)
                .ForSqlServerIsClustered(false);

            builder.HasIndex(model => model.OrganizationId)
                .ForSqlServerIsClustered(false);
        }
    }
}