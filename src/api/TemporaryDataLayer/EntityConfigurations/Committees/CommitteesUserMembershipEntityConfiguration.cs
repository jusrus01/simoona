using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryDataLayer.Models.Comittees;

namespace TemporaryDataLayer.EntityConfigurations.Committees
{
    public class CommitteesUserMembershipEntityConfiguration : IEntityTypeConfiguration<CommitteesUserMembership>
    {
        public void Configure(EntityTypeBuilder<CommitteesUserMembership> builder)
        {
            builder.ToTable("CommitteesUsersMembership");

            builder.ConfigureManyToMany(
                model => new { model.ApplicationUserId, model.CommitteeId },
                "CommitteesUsersMembership",
                model => model.CommitteesUserMembership,
                model => model.CommitteeId,
                model => model.Committee,
                "Committee",
                "FK_dbo.CommitteeApplicationUsers_dbo.Committees_Committee_Id",
                model => model.CommitteesUserMembership,
                model => model.ApplicationUserId,
                model => model.ApplicationUser,
                "ApplicationUser",
                "FK_dbo.CommitteeApplicationUsers_dbo.AspNetUsers_ApplicationUser_Id");
        }
    }
}
