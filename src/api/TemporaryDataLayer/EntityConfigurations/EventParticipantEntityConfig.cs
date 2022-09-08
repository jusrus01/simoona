﻿namespace TemporaryDataLayer
{
    internal class EventParticipantEntityConfig : EntityTypeConfiguration<EventParticipant>
    {
        public EventParticipantEntityConfig()
        {
            Map(e => e.Requires("IsDeleted").HasValue(false));

            HasRequired(e => e.ApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.ApplicationUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
