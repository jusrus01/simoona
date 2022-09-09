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
            builder.HasKey(model => new { model.Module_Id, model.Organization_Id });

            builder.HasOne(model => model.Module)
                .WithMany(model => model.ModuleOrganizations)
                .HasForeignKey(model => model.Module_Id);

            builder.HasOne(model => model.Organization)
                .WithMany(model => model.ModuleOrganizations)
                .HasForeignKey(model => model.Organization_Id);
        }
    }
}
