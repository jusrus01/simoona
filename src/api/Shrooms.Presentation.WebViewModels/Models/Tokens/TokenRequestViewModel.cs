using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Shrooms.Presentation.WebViewModels.Models.Tokens
{
    public class TokenRequestViewModel
    {
        [Required]
        [BindProperty(Name = "grant_type")]
        public string GrantType { get; set; }

        [Required]
        [BindProperty(Name = "username")]
        public string Username { get; set; }

        [Required]
        [BindProperty(Name = "password")]
        public string Password { get; set; }

        [Required]
        [BindProperty(Name = "client_id")]
        public string ClientId { get; set; }
    }
}
