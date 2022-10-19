using System.Collections.Generic;

namespace Shrooms.Contracts.DataTransferObjects.Models.Users
{
    public class LoggedInUserInfoDto
    {
        public bool Impersonated { get; set; }

        public object OrganizationId { get; set; }

        public object OrganizationName { get; set; }

        public IEnumerable<string> Permissions { get; set; }

        public IList<string> Roles { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string CultureCode { get; set; }

        public string TimeZone { get; set; }

        public string PictureId { get; set; }

        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }
}
