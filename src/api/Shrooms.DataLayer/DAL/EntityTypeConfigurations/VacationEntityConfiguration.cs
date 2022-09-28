using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class VacationEntityConfiguration : IEntityTypeConfiguration<VacationPage>
    {
        public void Configure(EntityTypeBuilder<VacationPage> builder)
        {
            builder.AddOrganization(DeleteBehavior.Cascade);
            builder.AddDefaultBaseModelConfiguration();

            builder.Property(model => model.Content)
                .IsRequired();
        }
    }
}