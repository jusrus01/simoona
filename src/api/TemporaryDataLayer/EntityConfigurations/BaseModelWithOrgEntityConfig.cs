using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    internal class BaseModelWithOrgEntityConfig : IEntityTypeConfiguration<BaseModelWithOrg>
    {
        public void Configure(EntityTypeBuilder<BaseModelWithOrg> builder)
        {
            //builder.HasOne(model => model.Organization)
            //    .WithMany()
            //    .HasForeignKey(model => model.OrganizationId)
            //    .IsRequired();
        }
    }
}
