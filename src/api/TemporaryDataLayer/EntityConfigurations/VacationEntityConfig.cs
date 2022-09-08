namespace TemporaryDataLayer
{
    public class VacationEntityConfig : EntityTypeConfiguration<VacationPage>
    {
        public VacationEntityConfig()
        {
            Property(v => v.Content)
                .IsRequired();
        }
    }
}