//namespace TemporaryDataLayer
//{
//    internal class NotifiationUserConfig : EntityTypeConfiguration<NotificationUser>
//    {
//        public NotifiationUserConfig()
//        {
//            HasKey(x => new { x.NotificationId, x.UserId });

//            Property(x => x.IsAlreadySeen)
//            .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_notification_IsAlreadySeen")));
//        }
//    }
//}
