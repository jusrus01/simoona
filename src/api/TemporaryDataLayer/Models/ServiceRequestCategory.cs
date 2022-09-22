using System.Collections.Generic;
using System.Linq;
using TemporaryDataLayer.Models;

namespace TemporaryDataLayer
{
    public class ServiceRequestCategory : BaseModel
    {
        public string Name { get; set; }

        public virtual IEnumerable<ApplicationUser> Assignees 
        {
            get => ServiceRequestCategoryApplicationUsers.Select(model => model.ApplicationUser);
        }

        // Required for many-to-many
        internal virtual ICollection<ServiceRequestCategoryApplicationUser> ServiceRequestCategoryApplicationUsers { get; set; }
    }
}
