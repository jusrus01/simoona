namespace Shrooms.Contracts.DataTransferObjects.Models.Users
{
    public class ExternalLoginRequestDto
    {
        public string Provider { get; set; }

        public string ClientId { get; set; }

        public string UserId { get; set; }

        public bool IsRegistration { get; set; }

        public string Error { get; set; }
    }
}
