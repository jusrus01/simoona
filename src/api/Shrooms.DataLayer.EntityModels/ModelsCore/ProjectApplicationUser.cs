namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class ProjectApplicationUser
    {
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
