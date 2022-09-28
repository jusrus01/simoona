using System.Collections.Generic;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class QualificationLevel : BaseModelWithOrg
    {
        public string Name { get; set; }

        public int SortOrder { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}