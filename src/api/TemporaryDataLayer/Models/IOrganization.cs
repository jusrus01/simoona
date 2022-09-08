namespace TemporaryDataLayer
{
    public interface IOrganization
    {
        int OrganizationId { get; set; }

        Organization Organization { get; set; }
    }
}
