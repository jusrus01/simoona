using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Enums;

namespace TemporaryDataLayer
{
    public class NotificationEntityConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.MapRequiredOrganization(DeleteBehavior.Cascade);
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete();

            builder.OwnsOne(model => model.Sources)
                .Property(model => model.Serialized)
                .HasColumnName("Sources");

            builder.Property(model => model.Type)
                .HasDefaultValue(NotificationType.NewEvent); // ?
        }
    }
}