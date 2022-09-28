namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class ServiceRequestCategoryApplicationUser
    {
        public int ServiceRequestCategoryId { get; set; }

        public ServiceRequestCategory ServiceRequestCategory { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
