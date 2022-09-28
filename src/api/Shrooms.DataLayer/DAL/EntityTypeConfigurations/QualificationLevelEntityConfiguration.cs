using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class QualificationLevelEntityConfiguration : IEntityTypeConfiguration<QualificationLevel>
    {
        public void Configure(EntityTypeBuilder<QualificationLevel> builder)
        {
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration();
        }
    }
}
