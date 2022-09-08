namespace TemporaryDataLayer
{
    public class PostWatcherConfig : EntityTypeConfiguration<PostWatcher>
    {
        public PostWatcherConfig()
        {
            ToTable("PostWatchers", "dbo");
        }
    }
}
