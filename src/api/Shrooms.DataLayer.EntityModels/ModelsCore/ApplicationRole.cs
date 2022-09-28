using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class ApplicationRole : IdentityRole, ISoftDelete, IOrganization
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName, int organizationId)
            : base(roleName)
        {
            OrganizationId = organizationId;
            CreatedTime = DateTime.UtcNow;
        }

        public new string Name
        {
            get => base.Name;
            set => base.Name = value;
        }

        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }

        public virtual IEnumerable<Permission> Permissions 
        {
            get => RolePermissions.Select(model => model.Permission);
        }

        public DateTime CreatedTime { get; set; }

        // Required for many-to-many
        internal ICollection<RolePermission> RolePermissions { get; set; }
    }
}
