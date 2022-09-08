namespace TemporaryDataLayer
{
    internal class PageEntityConfig : EntityTypeConfiguration<Page>
    {
        public PageEntityConfig()
        {
            Map(e => e.Requires("IsDeleted").HasValue(false))
                .HasOptional(e => e.ParentPage);

            HasRequired(p => p.Organization)
                .WithMany()
                .HasForeignKey(p => p.OrganizationId)
                .WillCascadeOnDelete(false);
        }
    }
}
