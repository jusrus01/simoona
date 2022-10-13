using System;

namespace Shrooms.Contracts.DataTransferObjects.Models.Tokens
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; }

        public string UserIndentifier { get; set; }

        public DateTime Expires { get; set; }

        public DateTime Issued { get; set; }

        public string ClientId { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string Persistent { get; set; }
    }
}
