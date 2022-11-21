namespace Shrooms.Contracts.DataTransferObjects.Models.Users
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}