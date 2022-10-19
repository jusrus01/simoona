namespace Shrooms.Contracts.Options
{
    public class ShroomsAuthenticationOptions
    {
        public JwtAuthenticationOptions Jwt { get; set; }

        public GoogleAuthenticationOptions Google { get; set; }

        public BasicAuthenticationOptions Basic { get; set; }
    }
}
