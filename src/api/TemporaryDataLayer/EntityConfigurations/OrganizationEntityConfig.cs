using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Constants;

namespace TemporaryDataLayer
{
    public class OrganizationEntityConfig : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.AddSoftDelete();

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
