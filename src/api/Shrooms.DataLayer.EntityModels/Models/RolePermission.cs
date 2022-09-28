﻿namespace Shrooms.DataLayer.EntityModels.Models
{
    public class RolePermission
    {
        public string RoleId { get; set; }

        public ApplicationRole Role { get; set; }

        public int PermissionId { get; set; }

        public Permission Permission { get; set; }
    }
}
