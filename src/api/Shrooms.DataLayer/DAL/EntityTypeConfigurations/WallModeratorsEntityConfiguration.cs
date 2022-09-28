using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Multiwalls;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class WallModeratorsEntityConfiguration : IEntityTypeConfiguration<WallModerator>
    {
        public void Configure(EntityTypeBuilder<WallModerator> builder)
        {
            builder.AddDefaultBaseModelConfiguration();
            builder.AddSoftDelete(true);

            builder.HasOne(model => model.User)
                .WithMany()
                .HasForeignKey(model => model.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.WallModerators_dbo.AspNetUsers_UserId");
        }
    }
}