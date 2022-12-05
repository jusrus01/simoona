using Newtonsoft.Json;

namespace Shrooms.Presentation.WebViewModels.Models.AccountModels
{
    public class ExternalLoginRequestViewModel//TODO: Add more validation
    {
        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("isRegistration")]
        public bool IsRegistration { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
