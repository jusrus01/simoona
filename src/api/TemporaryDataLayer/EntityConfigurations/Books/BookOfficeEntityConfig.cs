using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class BookOfficeEntityConfig : IEntityTypeConfiguration<BookOffice>
    {
        public void Configure(EntityTypeBuilder<BookOffice> builder)
        {
            builder.AddSoftDelete();

            builder.Property(u => u.ModifiedBy)
                .HasMaxLength(50);

            builder.Property(u => u.CreatedBy)
                .HasMaxLength(50);
        }
    }
}
