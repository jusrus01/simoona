namespace TemporaryDataLayer
{
    internal class IdentityUserClaimEntityConfig : EntityTypeConfiguration<IdentityUserClaim>
    {
        public IdentityUserClaimEntityConfig()
        {
            ToTable("AspNetUserClaims");
        }
    }
}
