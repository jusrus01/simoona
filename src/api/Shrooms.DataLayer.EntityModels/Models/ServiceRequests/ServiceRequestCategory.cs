using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.Models.ServiceRequests
{
    public class ServiceRequestCategory : BaseModel
    {
        public string Name { get; set; }

        public virtual IEnumerable<ApplicationUser> Assignees 
        {
            get => ServiceRequestCategoryApplicationUsers.Select(model => model.ApplicationUser);
        }

        // Required for many-to-many
        public ICollection<ServiceRequestCategoryApplicationUser> ServiceRequestCategoryApplicationUsers { get; set; }
    }
}
