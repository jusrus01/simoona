using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class CertificateEntityConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            //builder.HasDiscriminator("ClassificatorType", typeof(string))
            //    .HasValue("Certificate");
        }
    }
}
