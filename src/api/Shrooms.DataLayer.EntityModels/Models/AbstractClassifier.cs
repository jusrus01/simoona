using System.Collections.Generic;

namespace Shrooms.DataLayer.EntityModels.Models
{
    public abstract class AbstractClassifier : BaseModelWithOrg
    {
        public virtual string Name { get; set; }

        public virtual string Value { get; set; }

        public virtual int? ParentId { get; set; }

        public virtual AbstractClassifier Parent { get; set; }

        public virtual string SortOrder { get; set; }

        public virtual ICollection<AbstractClassifier> Children { get; set; }
    }
}