using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Shrooms.Presentation.WebViewModels.Models.Tokens
{
    public class TokenResponseViewModel
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("userIdentifier")]
        public string UserIndentifier { get; set; }
    }
}
