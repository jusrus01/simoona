using System.Collections.Generic;
using System.Linq;
using TemporaryDataLayer.Models;

namespace TemporaryDataLayer
{
    public class Module : BaseModel
    {
        public string Name { get; set; }

        public IEnumerable<Organization> Organizations 
        {
            get => ModuleOrganizations.Select(model => model.Organization);
        }

        // Required for many-to-many
        internal ICollection<ModuleOrganization> ModuleOrganizations { get; set; }
    }
}
