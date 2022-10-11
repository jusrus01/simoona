namespace Shrooms.Contracts.DataTransferObjects.Models.Tokens
{
    public class TokenRequestDto
    {
        public string GrantType { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ClientId { get; set; }
    }
}
