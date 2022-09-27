using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class NotificationsSettingsEntityConfiguration : IEntityTypeConfiguration<NotificationsSettings>
    {
        public void Configure(EntityTypeBuilder<NotificationsSettings> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete();

            builder.HasOne(model => model.ApplicationUser)
                .WithOne(model => model.NotificationsSettings)
                .HasForeignKey<NotificationsSettings>("ApplicationUser_Id")
                .HasConstraintName("FK_dbo.NotificationsSettings_dbo.AspNetUsers_ApplicationUser_Id")
                .IsRequired(false);

            builder.HasIndex("ApplicationUser_Id")
                .ForSqlServerIsClustered(false)
                .HasName("IX_ApplicationUser_Id")
                .IsUnique(false);

            builder.Property(model => model.MyPostsAppNotifications)
                .HasDefaultValue(false);

            builder.Property(model => model.MyPostsEmailNotifications)
                .HasDefaultValue(false);

            builder.Property(model => model.FollowingPostsAppNotifications)
                .HasDefaultValue(false);

            builder.Property(model => model.FollowingPostsEmailNotifications)
                .HasDefaultValue(false);

            builder.Property(model => model.EventWeeklyReminderAppNotifications)
                .HasDefaultValue(false);

            builder.Property(model => model.EventWeeklyReminderEmailNotifications)
                .HasDefaultValue(false);

            builder.Property(model => model.MentionEmailNotifications)
                .HasDefaultValue(false);

            builder.Property(model => model.CreatedLotteryEmailNotifications)
                .HasDefaultValue(false);
        }
    }
}
