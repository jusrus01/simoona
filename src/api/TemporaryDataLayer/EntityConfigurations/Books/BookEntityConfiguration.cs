using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.AddSoftDelete();
            builder.AddDefaultBaseModelConfiguration();
            builder.MapRequiredOrganization();

            builder.Property(u => u.Code)
              .HasMaxLength(20);

            builder.Property(u => u.Title)
                .IsRequired();

            builder.Property(u => u.Author)
               .IsRequired();

            builder.Property(u => u.Url)
                .HasMaxLength(2000);

            builder.Property(u => u.ModifiedBy)
                .HasMaxLength(50);

            builder.Property(u => u.CreatedBy)
                .HasMaxLength(50);

            builder.Property(u => u.Note)
                .HasMaxLength(9000);

            builder.HasOne(model => model.ApplicationUser)
                .WithMany()
                .HasForeignKey(model => model.ApplicationUserId)
                .HasConstraintName("FK_dbo.Books_dbo.AspNetUsers_ApplicationUserId");
        }
    }
}
