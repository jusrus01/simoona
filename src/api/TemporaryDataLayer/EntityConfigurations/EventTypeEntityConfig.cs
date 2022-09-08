﻿namespace TemporaryDataLayer
{
    internal class EventTypeEntityConfig : EntityTypeConfiguration<EventType>
    {
        public EventTypeEntityConfig()
        {
            Map(e => e.Requires("IsDeleted").HasValue(false));

            HasMany(r => r.Events)
                .WithRequired(e => e.EventType)
                .HasForeignKey(e => e.EventTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Organization)
                .WithMany()
                .WillCascadeOnDelete(false);

            Property(e => e.SingleJoinGroupName).HasMaxLength(value: 100);
        }
    }
}
