namespace TemporaryDataLayer
{
    internal class WallModeratorsConfiguration : EntityTypeConfiguration<WallModerator>
    {
        public WallModeratorsConfiguration()
        {
            Map(e => e.Requires("IsDeleted").HasValue(false));
        }
    }
}