namespace Shrooms.Contracts.DataTransferObjects.Models.Users
{
    public class VerifyEmailDto
    {
        public string Email { get; set; }

        public string Code { get; set; }
    }
}
