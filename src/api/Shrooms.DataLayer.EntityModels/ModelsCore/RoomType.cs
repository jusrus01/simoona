using System.Collections.Generic;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class RoomType : BaseModelWithOrg
    {
        public const string DefaultColor = "#FFFFFF";

        public string Name { get; set; }

        public string IconId { get; set; }

        public bool IsWorkingRoom { get; set; }

        public string Color { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}