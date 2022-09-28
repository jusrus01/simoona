namespace Shrooms.DataLayer.EntityModels.ModelsCore.Multiwalls
{
    public class WallModerator : BaseModel
    {
        public int WallId { get; set; }

        public Wall Wall { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
