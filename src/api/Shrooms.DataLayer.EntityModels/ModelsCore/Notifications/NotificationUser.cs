namespace Shrooms.DataLayer.EntityModels.ModelsCore.Notifications
{
    public class NotificationUser
    {
        public int NotificationId { get; set; }

        public virtual Notification Notification { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsAlreadySeen { get; set; }
    }
}
