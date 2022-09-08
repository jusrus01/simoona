namespace TemporaryDataLayer
{
    internal class BadgeCategoryKudosTypeEntityConfiguration : EntityTypeConfiguration<BadgeCategoryKudosType>
    {
        public BadgeCategoryKudosTypeEntityConfiguration()
        {
            HasRequired(x => x.BadgeCategory)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);

            HasRequired(x => x.KudosType)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);

            HasKey(type => type.Id);
        }
    }
}
