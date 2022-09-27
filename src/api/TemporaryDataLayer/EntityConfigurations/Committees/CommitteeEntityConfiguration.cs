using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
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
