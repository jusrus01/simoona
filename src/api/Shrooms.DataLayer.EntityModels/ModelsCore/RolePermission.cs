﻿namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class RolePermission
    {
        public string RoleId { get; set; }

        public ApplicationRole Role { get; set; }

        public int PermissionId { get; set; }

        public Permission Permission { get; set; }
    }
}
