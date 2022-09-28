using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class Module : BaseModel
    {
        public string Name { get; set; }

        public IEnumerable<Organization> Organizations 
        {
            get => ShroomsModuleOrganizations.Select(model => model.Organization);
        }

        // Required for many-to-many
        internal ICollection<ModuleOrganization> ShroomsModuleOrganizations { get; set; }
    }
}
