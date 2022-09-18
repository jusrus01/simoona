using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class VacationEntityConfiguration : IEntityTypeConfiguration<VacationPage>
    {
        public void Configure(EntityTypeBuilder<VacationPage> builder)
        {
            builder.MapRequiredOrganization(DeleteBehavior.Cascade);
            builder.AddDefaultBaseModelConfiguration();

            builder.Property(model => model.Content)
                .IsRequired();
        }
    }
}