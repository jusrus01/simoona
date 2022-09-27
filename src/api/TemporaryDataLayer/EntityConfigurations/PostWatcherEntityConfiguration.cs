using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    public class PostWatcherEntityConfiguration : IEntityTypeConfiguration<PostWatcher>
    {
        public void Configure(EntityTypeBuilder<PostWatcher> builder)
        {
            builder.HasKey(model => new { model.PostId, model.UserId });

            builder.ToTable("PostWatchers", "dbo");

            builder.HasOne(model => model.User)
                .WithMany()
                .HasForeignKey(model => model.UserId)
                .HasConstraintName("FK_dbo.PostWatchers_dbo.AspNetUsers_UserId");

            builder.HasIndex(model => model.PostId)
                .ForSqlServerIsClustered(false)
                .HasName("IX_PostId");
        }
    }
}
