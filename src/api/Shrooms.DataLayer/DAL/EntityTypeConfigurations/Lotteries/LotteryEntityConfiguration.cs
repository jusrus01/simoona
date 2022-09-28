using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.Contracts.Constants;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Lotteries;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Lotteries
{
    public class LotteryEntityConfiguration : IEntityTypeConfiguration<Lottery>
    {
        public void Configure(EntityTypeBuilder<Lottery> builder)
        {
            builder.AddOrganization(DeleteBehavior.Cascade);
            builder.AddDefaultBaseModelConfiguration();

            builder.Property(model => model.EndDate)
                .HasColumnType("datetime");

            builder.AddImages(model => model.Images);

            builder.Property(model => model.IsRefundFailed)
                .HasDefaultValue(false);

            builder.Property(model => model.GiftedTicketLimit)
                .HasDefaultValue(0);

            builder.Property(model => model.Description)
                .HasMaxLength(ValidationConstants.MaxPostMessageBodyLength);
        }
    }
}
