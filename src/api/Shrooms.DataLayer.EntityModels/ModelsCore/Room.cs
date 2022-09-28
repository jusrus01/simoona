using System.Collections.Generic;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class Room : BaseModelWithOrg
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public string Coordinates { get; set; }

        public int? FloorId { get; set; }

        public virtual Floor Floor { get; set; }

        public int? RoomTypeId { get; set; }

        public virtual RoomType RoomType { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}