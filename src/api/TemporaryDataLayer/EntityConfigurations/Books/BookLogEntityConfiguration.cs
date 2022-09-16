using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations.Books
{
    public class BookLogEntityConfiguration : IEntityTypeConfiguration<BookLog>
    {
        public void Configure(EntityTypeBuilder<BookLog> builder)
        {
            builder.MapRequiredOrganization();
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete();

            builder.HasOne(model => model.ApplicationUser)
                .WithMany()
                .HasForeignKey(model => model.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.BookLogs_dbo.AspNetUsers_ApplicationUserId");

            builder.Property(model => model.TakenFrom)
                .HasColumnType("datetime");

            builder.Property(model => model.Returned)
                .HasColumnType("datetime");

            builder.HasIndex(model => model.ApplicationUserId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_ApplicationUserId");

            builder.Property(model => model.ApplicationUserId)
                .IsRequired();

            builder.Property(model => model.ModifiedBy)
                .HasMaxLength(50);

            builder.Property(model => model.CreatedBy)
                .HasMaxLength(50);

            builder.Property(model => model.BookOfficeId)
                .HasDefaultValue(0);
        }
    }
}
