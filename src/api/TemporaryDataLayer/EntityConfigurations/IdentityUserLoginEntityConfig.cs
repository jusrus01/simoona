namespace TemporaryDataLayer
{
    internal class IdentityUserLoginEntityConfig : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginEntityConfig()
        {
            HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("AspNetUserLogins");
        }
    }
}
