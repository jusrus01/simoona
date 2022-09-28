using Shrooms.DataLayer.EntityModels.ModelsCore.Kudos;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class ServiceRequest : BaseModelWithOrg
    {
        public string EmployeeId { get; set; }

        public ApplicationUser Employee { get; set; }

        public string Title { get; set; }

        public int PriorityId { get; set; }

        public ServiceRequestPriority Priority { get; set; }

        public int StatusId { get; set; }

        public ServiceRequestStatus Status { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public int? KudosAmmount { get; set; }

        public int? KudosShopItemId { get; set; }

        public KudosShopItem KudosShopItem { get; set; }

        public string PictureId { get; set; }
    }
}
