using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class EventTypeEntityConfiguration : IEntityTypeConfiguration<EventType>
    {
        public void Configure(EntityTypeBuilder<EventType> builder)
        {
            builder.AddSoftDelete(false);
            builder.AddOrganization();
            builder.AddDefaultBaseModelConfiguration();

            builder.Property(model => model.SingleJoinGroupName)
                .HasMaxLength(100);

            builder.Property(model => model.SendWeeklyReminders)
                .HasDefaultValue(false);

            builder.Property(model => model.IsShownWithMainEvents)
                .HasDefaultValue(true);

            builder.Property(model => model.SendEmailToManager)
                .HasDefaultValue(false);
        }
    }
}
