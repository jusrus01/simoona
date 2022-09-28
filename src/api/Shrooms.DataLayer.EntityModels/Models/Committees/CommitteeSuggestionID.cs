namespace Shrooms.DataLayer.EntityModels.Models.Committees
{
    public class CommitteeSuggestionID
    {
        public int CommitteeId { get; set; }

        public Committee Committee { get; set; }

        public int CommitteeSuggestionId { get; set; }

        public CommitteeSuggestion CommitteeSuggestion { get; set; }
    }
}
