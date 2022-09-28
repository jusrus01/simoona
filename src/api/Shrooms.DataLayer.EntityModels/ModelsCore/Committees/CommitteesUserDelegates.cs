namespace Shrooms.DataLayer.EntityModels.ModelsCore.Committees
{
    public class CommitteesUserDelegates
    {
        public int CommitteeId { get; set; }

        public Committee Committee { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
