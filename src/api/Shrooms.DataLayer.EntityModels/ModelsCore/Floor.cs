using System.Collections.Generic;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class Floor : BaseModelWithOrg
    {
        public string Name { get; set; }

        public int? OfficeId { get; set; }

        public virtual Office Office { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public string PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}