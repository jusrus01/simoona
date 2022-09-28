using Shrooms.DataLayer.EntityModels.ModelsCore.Multiwalls;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public interface ILikeable
    {
        LikesCollection Likes { get; set; }
    }
}
