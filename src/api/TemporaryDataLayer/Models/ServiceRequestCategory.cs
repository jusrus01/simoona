using System.Collections.Generic;

namespace TemporaryDataLayer
{
    public class ServiceRequestCategory : BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Assignees { get; set; }
    }
}
