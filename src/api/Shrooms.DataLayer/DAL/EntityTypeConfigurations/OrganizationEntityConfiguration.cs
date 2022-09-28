using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Constants;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class OrganizationEntityConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.AddDefaultBaseModelConfiguration();

            builder.Property(model => model.Created)
                .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.Property(model => model.Modified)
                .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.Property(model => model.HasRestrictedAccess)
                .HasDefaultValue(false);

            builder.HasKey(model => model.Id);

            builder.AddSoftDelete(true);

            builder.Property(model => model.WelcomeEmail)
                .HasDefaultValue("<p style=\"text - align:center; font - size:14px; font - weight:400; margin: 0 0 0 0; \">Administrator has confirmed your registration</p>");

            builder.Property(model => model.RequiresUserConfirmation)
                .HasDefaultValue(false);

            builder.Property(model => model.TimeZone)
                .HasDefaultValue("FLE Standard Time");

            builder.Property(model => model.CultureCode)
                .HasDefaultValue("en-US");

            builder.Ignore(model => model.ShroomsModules);

            builder.Property(model => model.Name)
                .HasMaxLength(BusinessLayerConstants.MaxOrganizationNameLength)
                .IsRequired();

            builder.Property(model => model.ShortName)
                .HasMaxLength(BusinessLayerConstants.MaxOrganizationShortNameLength)
                .IsRequired();

            builder.Property(model => model.HostName)
                .HasMaxLength(BusinessLayerConstants.MaxHostNameLength);

            builder.Property(model => model.HasRestrictedAccess)
                .IsRequired();

            builder.Property(model => model.WelcomeEmail)
                .HasMaxLength(BusinessLayerConstants.WelcomeEmailLength)
                .IsRequired();
        }
    }
}
