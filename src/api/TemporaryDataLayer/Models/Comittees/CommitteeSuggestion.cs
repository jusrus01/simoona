using System;
using System.Collections.Generic;
using TemporaryDataLayer.Models.Comittees;

namespace TemporaryDataLayer
{
    public class CommitteeSuggestion : BaseModel
    {
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public virtual ApplicationUser User { get; set; }

        // Required for many-to-many
        internal ICollection<CommitteeSuggestionID> CommitteeSuggestionIds { get; set; }
    }
}
