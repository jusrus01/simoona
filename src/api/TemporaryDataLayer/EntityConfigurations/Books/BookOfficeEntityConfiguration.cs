using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class BookOfficeEntityConfiguration : IEntityTypeConfiguration<BookOffice>
    {
        public void Configure(EntityTypeBuilder<BookOffice> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.MapRequiredOrganization();
            builder.AddSoftDelete();

            builder.Property(u => u.ModifiedBy)
                .HasMaxLength(50);

            builder.Property(u => u.CreatedBy)
                .HasMaxLength(50);

            builder.HasIndex(model => new { model.BookId, model.OfficeId })
                .IsUnique()
                .HasName("BookId_OfficeId");

            builder.HasOne(model => model.Office)
                .WithMany()
                .HasForeignKey(model => model.OfficeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(model => model.BookLogs)
                .WithOne(model => model.BookOffice)
                .HasForeignKey(model => model.BookOfficeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
