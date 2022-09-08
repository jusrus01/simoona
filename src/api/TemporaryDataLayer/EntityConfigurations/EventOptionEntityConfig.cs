namespace TemporaryDataLayer
{
    public class EventOptionEntityConfig : EntityTypeConfiguration<EventOption>
    {
        public EventOptionEntityConfig()
        {
            Map(e => e.Requires("IsDeleted").HasValue(false));

            Property(e => e.Option)
                .IsRequired();
        }
    }
}
