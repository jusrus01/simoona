using System.Collections.Generic;

namespace TemporaryDataLayer
{
    public class QualificationLevel : BaseModelWithOrg
    {
        public string Name { get; set; }

        public int SortOrder { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}