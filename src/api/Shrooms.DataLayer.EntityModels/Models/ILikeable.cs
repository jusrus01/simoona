using Shrooms.DataLayer.EntityModels.Models.Multiwalls;

namespace Shrooms.DataLayer.EntityModels.Models
{
    public interface ILikeable
    {
        LikesCollection Likes { get; set; }
    }
}
