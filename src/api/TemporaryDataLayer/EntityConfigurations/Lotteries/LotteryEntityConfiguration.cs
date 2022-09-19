using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Constants;

namespace TemporaryDataLayer.EntityConfigurations.Lotteries
{
    public class LotteryEntityConfiguration : IEntityTypeConfiguration<Lottery>
    {
        public void Configure(EntityTypeBuilder<Lottery> builder)
        {
            builder.MapRequiredOrganization(DeleteBehavior.Cascade);
            builder.AddDefaultBaseModelConfiguration();

            builder.Property(model => model.EndDate)
                .HasColumnType("datetime");

            builder.OwnsOne(model => model.Images)
                .Property(model => model.Serialized)
                .HasColumnName("Images");

            builder.Property(model => model.IsRefundFailed)
                .HasDefaultValue(false);

            builder.Property(model => model.GiftedTicketLimit)
                .HasDefaultValue(0);

            builder.Property(model => model.Description)
                .HasMaxLength(ValidationConstants.MaxPostMessageBodyLength);
        }
    }
}
