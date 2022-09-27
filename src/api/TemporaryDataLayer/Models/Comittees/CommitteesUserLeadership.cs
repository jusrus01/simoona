namespace TemporaryDataLayer.Models.Comittees
{
    public class CommitteesUserLeadership
    {
        public int CommitteeId { get; set; }

        public Committee Committee { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
