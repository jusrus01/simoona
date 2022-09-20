using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(model => model.Id);
            
            builder.AddSoftDelete()
                .HasDefaultValue(null)
                .IsRequired();

            builder.Property(model => model.LastActivity)
                .HasDefaultValue(null)
                .IsRequired();

            builder.Property(model => model.WallId)
                .HasDefaultValue(null)
                .IsRequired();

            builder.Property(model => model.LastEdit)
                .HasDefaultValue("1900-01-01T00:00:00.000");

            builder.AddLikes(model => model.Likes);
            builder.AddImages(model => model.Images);

            builder.HasOne(model => model.Author)
                .WithMany()
                .HasForeignKey(model => model.AuthorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.Posts_dbo.AspNetUsers_ApplicationUserId");

            builder.AddDefaultBaseModelConfiguration();

            builder.HasIndex(model => model.LastActivity)
                .ForSqlServerIsClustered(false)
                .HasName("IX_LastActivity");
        }
    }
}
