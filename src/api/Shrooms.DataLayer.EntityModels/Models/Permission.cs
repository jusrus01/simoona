using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.Models
{
    public class Permission : BaseModel
    {
        public string Name { get; set; }

        public string Scope { get; set; }

        public virtual IEnumerable<ApplicationRole> Roles
        {
            get => RolePermissions.Select(model => model.Role); 
        }

        public virtual Module Module { get; set; }
 
        public int? ModuleId { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
