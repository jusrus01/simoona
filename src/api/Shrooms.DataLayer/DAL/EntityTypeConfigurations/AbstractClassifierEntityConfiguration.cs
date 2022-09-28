using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class AbstractClassifierEntityConfiguration : IEntityTypeConfiguration<AbstractClassifier>
    {
        public void Configure(EntityTypeBuilder<AbstractClassifier> builder)
        {
            builder.ToTable("AbstractClassifiers");

            builder.AddDefaultBaseModelConfiguration(true);
            builder.AddOrganization(foreignKeyName: "FK_dbo.Projects_dbo.Organizations_OrganizationId");
            builder.AddSoftDelete(true);

            builder.HasMany(model => model.Children)
                .WithOne(model => model.Parent)
                .HasForeignKey(model => model.ParentId);

            builder.HasDiscriminator<string>("ClassificatorType")
                .HasValue<Certificate>("Certificate")
                .HasValue<Language>("Language");

            builder.Property("ClassificatorType")
                .HasMaxLength(128)
                .HasDefaultValue("");
        }
    }
}
