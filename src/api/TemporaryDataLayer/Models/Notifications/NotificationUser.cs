using System.ComponentModel.DataAnnotations.Schema;

namespace TemporaryDataLayer
{
    public class NotificationUser
    {
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }

        public virtual Notification Notification { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsAlreadySeen { get; set; }
    }
}
