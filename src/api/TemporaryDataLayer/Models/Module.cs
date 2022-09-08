using System.Collections.Generic;

namespace TemporaryDataLayer
{
    public class Module : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
