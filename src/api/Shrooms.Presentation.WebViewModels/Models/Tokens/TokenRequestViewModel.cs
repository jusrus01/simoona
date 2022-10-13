using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Shrooms.Presentation.WebViewModels.Models.Tokens
{
    public class TokenRequestViewModel
    {
        [Required]
        [BindProperty(Name = "grant_type")]
        [JsonProperty("grant_type")]
        [FromForm(Name = "grant_type")]
        public string GrantType { get; set; }

        [Required]
        [BindProperty(Name = "username")]
        public string Username { get; set; }

        [Required]
        [BindProperty(Name = "password")]
        public string Password { get; set; }

        [Required]
        [BindProperty(Name = "client_id")]
        [FromForm(Name = "client_id")]
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}
