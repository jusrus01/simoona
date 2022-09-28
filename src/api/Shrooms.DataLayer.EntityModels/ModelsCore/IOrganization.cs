namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public interface IOrganization
    {
        int OrganizationId { get; set; }

        Organization Organization { get; set; }
    }
}
