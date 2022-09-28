using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class SyncTokenEntityConfiguration : IEntityTypeConfiguration<SyncToken>
    {
        public void Configure(EntityTypeBuilder<SyncToken> builder)
        {
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration();
        }
    }
}
