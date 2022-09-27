using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace TemporaryDataLayer
{
    public class EventParticipantEntityConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder.AddSoftDelete(true);
            builder.AddDefaultBaseModelConfiguration(true);

            builder.Property(model => model.ApplicationUserId)
                .HasDefaultValue("")
                .IsRequired();

            builder.Property(model => model.EventId)
                .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

            builder.Property(model => model.AttendStatus)
                .HasDefaultValue(1);

            builder.HasOne(model => model.Event)
                .WithMany(model => model.EventParticipants)
                .HasForeignKey(model => model.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex("IsDeleted")
                .ForSqlServerIsClustered(false)
                .HasName("nci_wi_EventParticipants_CA1F6B4699FAB2347B166CEA9639C7E8")
                .ForSqlServerInclude("EventId");
        }
    }
}
