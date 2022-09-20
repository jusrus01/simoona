using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(model => model.Id);

            builder.Property(model => model.Id)
                .HasDefaultValueSql("NEWID()"); // TODO: look for alternatives

            builder.HasOne(model => model.Organization)
                .WithMany()
                .HasForeignKey(model => model.OrganizationId);

            builder.Property(model => model.Created)
                .HasColumnType("datetime")
                .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.Property(model => model.Modified)
                .HasColumnType("datetime")
                .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.HasOne(model => model.Office)
                .WithMany()
                .HasForeignKey(model => model.OfficeId);

            builder.Property(model => model.MaxParticipants)
                .HasMaxLength(short.MaxValue);

            builder.Property(model => model.MaxChoices)
                .HasMaxLength(short.MaxValue);

            builder.HasOne(model => model.EventType)
                .WithMany(model => model.Events)
                .HasForeignKey(model => model.EventTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(model => model.ResponsibleUser)
                .WithMany()
                .HasForeignKey(model => model.ResponsibleUserId)
                .HasConstraintName("FK_dbo.Events_dbo.AspNetUsers_ResponsibleUserId");

            builder.Property(model => model.Offices)
                .IsRequired();

            builder.HasOne(model => model.Wall)
                .WithMany()
                .HasForeignKey(model => model.WallId)
                .IsRequired();

            builder.HasIndex(model => model.StartDate)
                .ForSqlServerIsClustered(false)
                .HasName("ix_start_date");

            builder.HasIndex(model => model.EndDate)
                .ForSqlServerIsClustered(false)
                .HasName("ix_end_date");

            builder.Property(model => model.Name)
                .IsRequired();

            builder.Property(model => model.StartDate)
                .HasColumnType("datetime");

            builder.Property(model => model.MaxParticipants)
                .IsRequired();

            builder.AddSoftDelete(true);

            builder.Property(model => model.EndDate)
                .HasColumnType("datetime")
                .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.Property(model => model.RegistrationDeadline)
                .HasColumnType("datetime")
                .HasDefaultValue("2016-05-11T08:57:02.755Z");

            builder.Property(model => model.AllowMaybeGoing)
                .HasDefaultValue(true);

            builder.Property(model => model.AllowNotGoing)
                .HasDefaultValue(true);

            builder.Ignore(model => model.OfficeIds);

            builder.Ignore(model => model.LocalStartDate);

            builder.Ignore(model => model.LocalEndDate);

            builder.Ignore(model => model.LocalRegistrationDeadline);
        }
    }
}
