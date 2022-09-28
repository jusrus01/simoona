using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Multiwalls;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class WallMemberEntityConfiguration : IEntityTypeConfiguration<WallMember>
    {
        public void Configure(EntityTypeBuilder<WallMember> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete();

            builder.HasOne(model => model.User)
                .WithMany(model => model.WallUsers)
                .HasForeignKey(model => model.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.WallUsers_dbo.AspNetUsers_UserId");

            builder.Property(model => model.AppNotificationsEnabled)
                .HasDefaultValue(true);

            builder.Property(model => model.EmailNotificationsEnabled)
                .HasDefaultValue(true);

            builder.HasIndex("IsDeleted", "UserId") // TODO: export
                .ForSqlServerIsClustered(false)
                .HasName("nci_wi_WallMembers_6C8CE6B55B79BC00FDA53D9B579C2EFA");
        }
    }
}
