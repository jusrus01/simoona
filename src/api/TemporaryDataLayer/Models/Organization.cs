using System.Collections.Generic;
using TemporaryDataLayer.Models;
using System.Linq;

namespace TemporaryDataLayer
{
    public class Organization : BaseModel
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string HostName { get; set; }

        public bool HasRestrictedAccess { get; set; }

        public string WelcomeEmail { get; set; }

        public bool RequiresUserConfirmation { get; set; }

        public string CalendarId { get; set; }

        public string TimeZone { get; set; }

        public string CultureCode { get; set; }

        public string BookAppAuthorizationGuid { get; set; }

        public string AuthenticationProviders { get; set; }

        public string KudosYearlyMultipliers { get; set; }

        public IEnumerable<Module> ShroomsModules
        {
            get => ModuleOrganizations.Select(x => x.Module);
        }

        // Required for many-to-many
        internal ICollection<ModuleOrganization> ModuleOrganizations { get; set; }
    }
}
