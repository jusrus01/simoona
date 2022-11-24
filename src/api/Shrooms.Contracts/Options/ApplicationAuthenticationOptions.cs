namespace Shrooms.Contracts.Options
{
    public class ApplicationAuthenticationOptions
    {
        public JwtAuthenticationOptions Jwt { get; set; }

        public GoogleAuthenticationOptions Google { get; set; }

        public BasicAuthenticationOptions Basic { get; set; }
    }
}
