using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class AbstractClassifierEntityConfiguration : IEntityTypeConfiguration<AbstractClassifier>
    {
        public void Configure(EntityTypeBuilder<AbstractClassifier> builder)
        {
            builder.ToTable("AbstractClassifiers");

            builder.AddDefaultBaseModelConfiguration(true);
            builder.AddOrganization();
            builder.AddSoftDelete(true);



            builder.HasMany(model => model.Children)
                .WithOne(model => model.Parent)
                .HasForeignKey(model => model.ParentId);

            builder.HasDiscriminator<string>("ClassificatorType")
                .HasValue<Certificate>("Certificate")
                .HasValue<Language>("Language");

            builder.Property("ClassificatorType")
                .HasDefaultValue("");
        }
    }
}
