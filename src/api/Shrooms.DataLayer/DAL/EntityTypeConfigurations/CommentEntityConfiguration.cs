using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Multiwalls;

namespace Shrooms.DataLayer.EntityTypeConfigurations
{
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(model => model.Id);

            builder.HasOne(model => model.Author)
                .WithMany()
                .HasForeignKey(model => model.AuthorId)
                .HasConstraintName("FK_dbo.Comments_dbo.AspNetUsers_ApplicationUserId");

            builder.AddDefaultBaseModelConfiguration();

            builder.AddSoftDelete();

            builder.Property(model => model.PostId)
                .HasDefaultValue(0);

            builder.AddLikes(model => model.Likes);
            builder.AddImages(model => model.Images);

            builder.Property(model => model.IsHidden)
                .HasDefaultValue(false);

            builder.Property(model => model.LastEdit)
                .HasColumnType("datetime")
                .HasDefaultValue("1900-01-01T00:00:00.000");
        }
    }
}
