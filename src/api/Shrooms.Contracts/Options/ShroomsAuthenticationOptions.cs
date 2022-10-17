namespace Shrooms.Contracts.Options
{
    public class ShroomsAuthenticationOptions
    {
        public JwtOptions Jwt { get; set; }

        public GoogleOptions Google { get; set; }

        public BasicOptions Basic { get; set; }
    }
}
