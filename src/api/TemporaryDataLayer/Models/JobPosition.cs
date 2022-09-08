using System.Collections.Generic;

namespace TemporaryDataLayer
{
    public class JobPosition : BaseModelWithOrg
    {
        public string Title { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
