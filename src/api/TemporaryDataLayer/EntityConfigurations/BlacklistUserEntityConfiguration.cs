using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class BlacklistUserEntityConfiguration : IEntityTypeConfiguration<BlacklistUser>
    {
        public void Configure(EntityTypeBuilder<BlacklistUser> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddOrganization(DeleteBehavior.Cascade);

            builder.HasOne(model => model.CreatedByUser)
                .WithMany()
                .HasForeignKey(model => model.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.BlacklistUsers_dbo.AspNetUsers_CreatedBy");

            builder.HasOne(model => model.ModifiedByUser)
                .WithMany()
                .HasForeignKey(model => model.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.BlacklistUsers_dbo.AspNetUsers_ModifiedBy");

            builder.HasOne(model => model.User)
                .WithMany()
                .HasForeignKey(model => model.UserId)
                .HasConstraintName("FK_dbo.BlacklistUsers_dbo.AspNetUsers_UserId");

            builder.Property(model => model.EndDate)
                .HasColumnType("datetime");

            builder.HasIndex(model => model.CreatedBy)
                .ForSqlServerIsClustered(false)
                .HasName("IX_CreatedBy");

            builder.HasIndex(model => model.ModifiedBy)
                .ForSqlServerIsClustered(false)
                .HasName("IX_ModifiedBy");

            builder.Property(u => u.UserId)
                .IsRequired();

            builder.Property(u => u.EndDate)
                .IsRequired();

            builder.Property(u => u.Status)
                .IsRequired();

            builder.Property(u => u.ModifiedBy)
                .IsRequired();
        }
    }
}
