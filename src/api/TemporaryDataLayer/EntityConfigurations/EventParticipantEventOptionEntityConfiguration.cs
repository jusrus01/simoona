using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryDataLayer.Models.Events;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class EventParticipantEventOptionEntityConfiguration : IEntityTypeConfiguration<EventParticipantEventOption>
    {
        public void Configure(EntityTypeBuilder<EventParticipantEventOption> builder)
        {
            builder.ToTable("EventParticipantEventOptions");

            builder.Property(model => model.EventParticipantId)
                .HasColumnName("EventParticipant_Id");

            builder.Property(model => model.EventOptionId)
                .HasColumnName("EventOption_Id");

            builder.HasKey(model => new { model.EventParticipantId, model.EventOptionId })
                .HasName("PK_dbo.EventParticipantEventOptions");

            builder.HasOne(model => model.EventParticipant)
                .WithMany(model => model.EventParticipantEventOptions)
                .HasForeignKey(model => model.EventParticipantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipant_Id")
                .IsRequired();

            builder.HasOne(model => model.EventOption)
                .WithMany(model => model.EventParticipantEventOptions)
                .HasForeignKey(model => model.EventOptionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.EventParticipantEventOptions_dbo.EventOptions_EventOption_Id")
                .IsRequired();

            builder.HasIndex(model => model.EventOptionId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_EventOption_Id");

            builder.HasIndex(model => model.EventParticipantId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_EventParticipant_Id");
        }
    }
}
