namespace Shrooms.Contracts.Options
{
    public class JwtAuthenticationOptions
    {
        public string Key { get; set; }

        public int DurationInDays { get; set; }
    }
}
