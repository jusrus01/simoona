namespace Shrooms.DataLayer.EntityModels.Models.ServiceRequests
{
    public class ServiceRequestComment : BaseModelWithOrg
    {
        public string EmployeeId { get; set; }

        public ApplicationUser Employee { get; set; }

        public int ServiceRequestId { get; set; }

        public ServiceRequest ServiceRequest { get; set; }

        public string Content { get; set; }
    }
}
