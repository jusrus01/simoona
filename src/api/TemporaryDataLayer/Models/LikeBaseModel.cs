using System.Linq;

namespace TemporaryDataLayer
{
    public abstract class LikeBaseModel : BaseModel, ILikeable
    {
        public virtual LikesCollection Likes { get; set; }

        public bool IsLikedByUser(string userId)
        {
            return Likes.Any(a => a.UserId == userId);
        }
    }
}