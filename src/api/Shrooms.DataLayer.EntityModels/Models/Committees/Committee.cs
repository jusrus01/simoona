using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.Models.Committees
{
    public class Committee : BaseModelWithOrg
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureId { get; set; }

        public string Website { get; set; }

        public bool IsKudosCommittee { get; set; }

        public IEnumerable<ApplicationUser> Members 
        {
            get => CommitteesUserMembership.Select(model => model.ApplicationUser);
        }

        public IEnumerable<CommitteeSuggestion> Suggestions
        {
            get => CommitteeSuggestionIds.Select(model => model.CommitteeSuggestion);
        }

        public IEnumerable<ApplicationUser> Leads 
        {
            get => CommitteesUserLeadership.Select(model => model.ApplicationUser);
        }

        public virtual IEnumerable<ApplicationUser> Delegates 
        {
            get => CommitteesUserDelegates.Select(model => model.ApplicationUser);
        }

        // Required for many-to-many
        public ICollection<CommitteesUserMembership> CommitteesUserMembership { get; set; }

        public ICollection<CommitteesUserLeadership> CommitteesUserLeadership { get; set; }

        public ICollection<CommitteesUserDelegates> CommitteesUserDelegates { get; set; }

        public ICollection<CommitteeSuggestionID> CommitteeSuggestionIds { get; set; }
    }
}
