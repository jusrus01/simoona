using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryDataLayer.Models.Comittees;

namespace TemporaryDataLayer.EntityConfigurations.Committees
{
    public class CommitteesUserLeadershipEntityConfiguration : IEntityTypeConfiguration<CommitteesUserLeadership>
    {
        public void Configure(EntityTypeBuilder<CommitteesUserLeadership> builder)
        {
            builder.ToTable("CommitteesUsersLeadership");

            builder.ConfigureManyToMany(
                model => new { model.ApplicationUserId, model.CommitteeId },
                "CommitteesUsersLeadership",
                model => model.CommitteesUserLeadership,
                model => model.ApplicationUserId,
                model => model.ApplicationUser,
                "ApplicationUser",
                "FK_dbo.CommitteesUsersLeadership_dbo.AspNetUsers_ApplicationUser_Id",
                model => model.CommitteesUserLeadership,
                model => model.CommitteeId,
                model => model.Committee,
                "Committee",
                "FK_dbo.CommitteesUsersLeadership_dbo.Committees_Committee_Id");
        }
    }
}
