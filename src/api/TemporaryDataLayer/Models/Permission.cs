using System.Collections.Generic;
using System.Linq;
using TemporaryDataLayer.Models;

namespace TemporaryDataLayer
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

        //[ForeignKey("Module")]
        public int? ModuleId { get; set; }

        // Required for many-to-many
        internal ICollection<RolePermission> RolePermissions { get; set; }
    }
}
