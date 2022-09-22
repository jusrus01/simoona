using System.Collections.Generic;

namespace TemporaryDataLayer
{
    public class KudosBasket : BaseModelWithOrg
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<KudosLog> KudosLogs { get; set; }
    }
}
