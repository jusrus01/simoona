using System.ComponentModel.DataAnnotations.Schema;

namespace TemporaryDataLayer
{
    public class WallMember : BaseModel
    {
        public int WallId { get; set; }

        public Wall Wall { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool AppNotificationsEnabled { get; set; }

        public bool EmailNotificationsEnabled { get; set; }
    }
}
