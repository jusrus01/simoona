﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shrooms.Contracts.Constants;

namespace TemporaryDataLayer
{
    public class Organization : BaseModel
    {
        [Required]
        [StringLength(BusinessLayerConstants.MaxOrganizationNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(BusinessLayerConstants.MaxOrganizationShortNameLength)]
        public string ShortName { get; set; }

        [StringLength(50)]
        public string HostName { get; set; }

        [Required]
        public bool HasRestrictedAccess { get; set; }

        public virtual ICollection<Module> ShroomsModules { get; set; }

        [Required]
        [StringLength(BusinessLayerConstants.WelcomeEmailLength)]
        public string WelcomeEmail { get; set; }

        public bool RequiresUserConfirmation { get; set; }

        public string CalendarId { get; set; }

        public string TimeZone { get; set; }

        public string CultureCode { get; set; }

        public string BookAppAuthorizationGuid { get; set; }

        public string AuthenticationProviders { get; set; }

        public string KudosYearlyMultipliers { get; set; }
    }
}
