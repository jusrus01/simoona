using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Enums;
using System;

namespace TemporaryDataLayer
{
    public class EventOptionEntityConfiguration : IEntityTypeConfiguration<EventOption>
    {
        public void Configure(EntityTypeBuilder<EventOption> builder)
        {
            builder.AddSoftDelete();
            builder.AddDefaultBaseModelConfiguration();

            builder.Property(model => model.Option)
                .IsRequired();

            builder.Property(model => model.EventId)
                .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

            builder.Property(model => model.Rule)
                .HasDefaultValue(OptionRules.Default);

            builder.HasOne(model => model.Event)
                .WithMany(model => model.EventOptions)
                .HasForeignKey(model => model.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
