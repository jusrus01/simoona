namespace TemporaryDataLayer
{
    internal class KudosBasketEntityConfig : EntityTypeConfiguration<KudosBasket>
    {
        public KudosBasketEntityConfig()
        {
            Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(25);

            Property(b => b.Description)
                .HasMaxLength(5000);

            Map(m => m.Requires("IsDeleted")
                .HasValue(false));

            HasRequired(k => k.Organization)
                .WithMany()
                .HasForeignKey(k => k.OrganizationId)
                .WillCascadeOnDelete(false);
        }
    }
}
