namespace Shrooms.Contracts.DataTransferObjects.Models.Users
{
    public class LoginDto
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }

        public string ClientId { get; set; }
    }
}
