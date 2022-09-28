using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
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
