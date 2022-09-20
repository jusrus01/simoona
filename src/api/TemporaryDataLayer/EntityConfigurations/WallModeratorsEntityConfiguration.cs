using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    // TODO: update EF model to contain foreign keys
    public class WallModeratorsEntityConfiguration : IEntityTypeConfiguration<WallModerator>
    {
        public void Configure(EntityTypeBuilder<WallModerator> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete(true);

            builder.HasOne(model => model.Wall)
                .WithMany()
                .HasForeignKey(model => model.WallId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(model => model.User)
                .WithMany()
                .HasForeignKey(model => model.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.WallModerators_dbo.AspNetUsers_UserId");
        }
    }
}