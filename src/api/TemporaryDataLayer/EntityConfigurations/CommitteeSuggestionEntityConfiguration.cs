using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer.EntityConfigurations
{
    public class CommitteeSuggestionEntityConfiguration : IEntityTypeConfiguration<CommitteeSuggestion>
    {
        public void Configure(EntityTypeBuilder<CommitteeSuggestion> builder)
        {
            builder.AddSoftDelete();
            builder.AddDefaultBaseModelConfiguration();

            builder.Property(model => model.Date)
                .HasColumnType("datetime");

            builder.HasOne(model => model.User)
                .WithMany()
                .HasForeignKey("User_Id")
                .HasConstraintName("FK_dbo.CommitteeSuggestions_dbo.AspNetUsers_UserId");

            builder.HasIndex("User_Id")
                .ForSqlServerIsClustered(false)
                .HasName("IX_User_Id");
        }
    }
}
