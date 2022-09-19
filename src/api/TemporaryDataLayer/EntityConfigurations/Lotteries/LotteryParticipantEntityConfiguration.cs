using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations.Lotteries
{
    public class LotteryParticipantEntityConfiguration : IEntityTypeConfiguration<LotteryParticipant>
    {
        public void Configure(EntityTypeBuilder<LotteryParticipant> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.HasKey(model => model.Id);

            builder.Property(model => model.Joined)
                .HasColumnType("datetime");

            builder.HasOne(model => model.User)
                .WithMany()
                .HasForeignKey(model => model.UserId)
                .HasConstraintName("FK_dbo.LotteryParticipants_dbo.AspNetUsers_UserId");
        }
    }
}