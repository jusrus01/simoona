namespace Shrooms.DataLayer.EntityModels.Models.Committees
{
    public class CommitteesUserMembership
    {
        public int CommitteeId { get; set; }

        public Committee Committee { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
