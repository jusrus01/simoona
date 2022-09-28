using Shrooms.DataLayer.EntityModels.ModelsCore.Kudos;
using System.Collections.Generic;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class KudosBasket : BaseModelWithOrg
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<KudosLog> KudosLogs { get; set; }
    }
}
