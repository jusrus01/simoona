using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    internal class PageEntityConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.Property(model => model.Name)
                .IsRequired();

            builder.AddSoftDelete(true);
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration(true);
        }
    }
}
