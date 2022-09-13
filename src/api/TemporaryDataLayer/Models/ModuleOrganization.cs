namespace TemporaryDataLayer.Models
{
    public class ModuleOrganization
    {
        public int ModuleId { get; set; }

        public Module Module { get; set; }

        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}
