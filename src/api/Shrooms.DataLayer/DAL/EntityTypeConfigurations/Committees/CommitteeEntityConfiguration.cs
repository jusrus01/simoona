using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Committees;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Committees
{
    public class CommitteeEntityConfiguration : IEntityTypeConfiguration<Committee>
    {
        public void Configure(EntityTypeBuilder<Committee> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete();
            builder.AddOrganization();

            builder.Property(model => model.IsKudosCommittee)
                .HasDefaultValue(false);
        }
    }
}
