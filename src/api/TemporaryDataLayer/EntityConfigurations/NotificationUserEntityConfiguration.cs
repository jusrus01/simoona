using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class NotificationUserEntityConfiguration : IEntityTypeConfiguration<NotificationUser>
    {
        public void Configure(EntityTypeBuilder<NotificationUser> builder)
        {
            builder.HasKey(model => new { model.NotificationId, model.UserId })
                .HasName("PK_dbo.NotificationUsers");

            builder.HasIndex(model => model.NotificationId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_NotificationId");

            builder.HasIndex(model => model.IsAlreadySeen)
                .ForSqlServerIsClustered(false)
                .HasName("ix_notification_IsAlreadySeen");

            builder.HasOne(model => model.User)
                .WithMany()
                .HasForeignKey(model => model.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.NotificationUsers_dbo.AspNetUsers_UserId");
        }
    }
}
