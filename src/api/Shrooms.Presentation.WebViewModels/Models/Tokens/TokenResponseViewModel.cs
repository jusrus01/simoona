using System;
using System.Text.Json.Serialization;

namespace Shrooms.Presentation.WebViewModels.Models.Tokens
{
    public class TokenResponseViewModel
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("userIdentifier")]
        public string UserIndentifier { get; set; }

        [JsonPropertyName(".expires")]
        public DateTime Expires { get; set; }

        [JsonPropertyName(".issued")]
        public DateTime Issued { get; set; }

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName(".persistent")]
        public string Persistent { get; set; }
    }
}
