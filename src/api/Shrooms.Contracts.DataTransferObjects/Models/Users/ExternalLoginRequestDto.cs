using JetBrains.Annotations;

namespace Shrooms.Contracts.DataTransferObjects.Models.Users
{
    public class ExternalLoginRequestDto
    {
        [CanBeNull]
        public string Provider { get; set; }

        [CanBeNull]
        public string ClientId { get; set; }

        [CanBeNull]
        public string UserId { get; set; }

        [CanBeNull]
        public bool IsRegistration { get; set; }

        [CanBeNull]
        public string Error { get; set; }
    }
}
