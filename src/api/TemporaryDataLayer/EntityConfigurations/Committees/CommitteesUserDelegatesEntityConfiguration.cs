using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryDataLayer.Models.Comittees;

namespace TemporaryDataLayer.EntityConfigurations.Committees
{
    public class CommitteesUserDelegatesEntityConfiguration : IEntityTypeConfiguration<CommitteesUserDelegates>
    {
        public void Configure(EntityTypeBuilder<CommitteesUserDelegates> builder)
        {
            builder.ToTable("CommitteesUsersDelegates");

            builder.ConfigureManyToMany(
                model => new { model.ApplicationUserId, model.CommitteeId },
                "CommitteesUsersDelegates",
                model => model.CommitteesUserDelegates,
                model => model.ApplicationUserId,
                model => model.ApplicationUser,
                "ApplicationUser",
                "FK_dbo.CommitteesUsersDelegates_dbo.AspNetUsers_ApplicationUser_Id",
                model => model.CommitteesUserDelegates,
                model => model.CommitteeId,
                model => model.Committee,
                "Committee",
                "FK_dbo.CommitteesUsersDelegates_dbo.Committees_Committee_Id");
        }
    }
}
