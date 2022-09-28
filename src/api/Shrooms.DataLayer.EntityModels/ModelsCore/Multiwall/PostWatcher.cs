namespace Shrooms.DataLayer.EntityModels.ModelsCore.Multiwalls
{
    public class PostWatcher
    {
        public int PostId { get; set; }

        public string UserId { get; set; }

        public virtual Post Post { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
