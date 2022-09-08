﻿namespace TemporaryDataLayer
{
    internal class EventEntityConfig : EntityTypeConfiguration<Event>
    {
        public EventEntityConfig()
        {
            Map(e => e.Requires("IsDeleted").HasValue(false));

            Property(e => e.Name)
                .IsRequired();

            Property(e => e.Place)
                .IsRequired();

            Property(e => e.MaxParticipants)
                .IsRequired();

            Property(e => e.StartDate)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                       new IndexAttribute("ix_start_date")));

            Property(e => e.EndDate)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                       new IndexAttribute("ix_end_date")));

            HasRequired(e => e.Organization)
                .WithMany()
                .HasForeignKey(x => x.OrganizationId)
                .WillCascadeOnDelete(false);

            HasMany(e => e.EventOptions)
                .WithRequired(e => e.Event)
                .HasForeignKey(e => e.EventId)
                .WillCascadeOnDelete(false);
        }
    }
}
