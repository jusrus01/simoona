namespace Shrooms.DataLayer.EntityModels.ModelsCore.Committees
{
    public class CommitteeSuggestionID
    {
        public int CommitteeId { get; set; }

        public Committee Committee { get; set; }

        public int CommitteeSuggestionId { get; set; }

        public CommitteeSuggestion CommitteeSuggestion { get; set; }
    }
}
