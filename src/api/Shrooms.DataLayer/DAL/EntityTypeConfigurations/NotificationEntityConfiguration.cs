using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Enums;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Notifications;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class NotificationEntityConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.AddOrganization(DeleteBehavior.Cascade);
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete();

            builder.OwnsOne(model => model.Sources)
                .Ignore(model => model.WallId)
                .Ignore(model => model.PostId)
                .Ignore(model => model.ProjectId)
                .Ignore(model => model.EventId)
                .Property(model => model.Serialized)
                .HasColumnName("Sources");

            builder.Property(model => model.Type)
                .HasDefaultValue(NotificationType.NewEvent); // ?
        }
    }
}