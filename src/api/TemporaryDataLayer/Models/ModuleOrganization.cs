namespace TemporaryDataLayer.Models
{
    public class ModuleOrganization
    {
        public int Module_Id { get; set; }

        public Module Module { get; set; }

        public int Organization_Id { get; set; }

        public Organization Organization { get; set; }
    }
}
