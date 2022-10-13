using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.Models.Permissions;
using Shrooms.Contracts.Infrastructure;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Domain.Services.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly DbSet<Permission> _permissionsDbSet;
        private readonly DbSet<IdentityUserRole<string>> _userRolesDbSet;
        private readonly DbSet<RolePermission> _rolePermissionsDbSet;

        private readonly ICustomCache<string, IList<string>> _permissionsCache;

        public PermissionService(IUnitOfWork2 unitOfWork, ICustomCache<string, IList<string>> permissionsCache)
        {
            _permissionsDbSet = unitOfWork.GetDbSet<Permission>();
            _userRolesDbSet = unitOfWork.GetDbSet<IdentityUserRole<string>>();
            _rolePermissionsDbSet = unitOfWork.GetDbSet<RolePermission>();

            _permissionsCache = permissionsCache;
        }

        public bool UserHasPermission(UserAndOrganizationDto userAndOrg, string permissionName)
        {
            throw new NotImplementedException();
            //if (!_permissionsCache.TryGetValue(userAndOrg.UserId, out var permissions))
            //{
            //    permissions = _permissionsDbSet
            //        .Where(p => p.Roles.Any(r => r.Users.Any(u => u.UserId == userAndOrg.UserId)))
            //        .Where(FilterActiveModules(userAndOrg.OrganizationId))
            //        .Select(x => x.Name)
            //        .ToList();

            //    _permissionsCache.TryAdd(userAndOrg.UserId, permissions);
            //}

            //var isPermitted = permissions.Contains(permissionName);
            //return isPermitted;
        }

        public async Task<bool> UserHasPermissionAsync(UserAndOrganizationDto userAndOrg, string permissionName)
        {
            throw new NotImplementedException();
            //if (!_permissionsCache.TryGetValue(userAndOrg.UserId, out var permissions))
            //{
            //    permissions = await _permissionsDbSet
            //        .Where(p => p.Roles.Any(r => r.Users.Any(u => u.UserId == userAndOrg.UserId)))
            //        .Where(FilterActiveModules(userAndOrg.OrganizationId))
            //        .Select(x => x.Name)
            //        .ToListAsync();

            //    _permissionsCache.TryAdd(userAndOrg.UserId, permissions);
            //}

            //var isPermitted = permissions.Contains(permissionName);
            //return isPermitted;
        }

        public async Task<IEnumerable<PermissionGroupDto>> GetGroupNamesAsync(int organizationId)
        {
            throw new NotImplementedException();
            //var allPermissions = await GetPermissionsAsync(organizationId);

            //return allPermissions
            //    .Select(x => new PermissionGroupDto
            //    {
            //        Name = x.Name.Split(DataLayerConstants.PermissionSplitter).First().ToLower()
            //    })
            //    .DistinctBy(x => x.Name)
            //    .OrderBy(x => x.Name)
            //    .ToList();
        }

        public async Task<IEnumerable<string>> GetUserPermissionsAsync(string userId)
        {
            if (_permissionsCache.TryGetValue(userId, out var permissions))
            {
                return permissions;
            }

            var userRoles = await _userRolesDbSet.Where(userRole => userRole.UserId == userId)
                .Select(userRole => userRole.RoleId)
                .ToListAsync();

            permissions = await _rolePermissionsDbSet.Where(rolePermission => userRoles.Contains(rolePermission.RoleId))
                .Select(rolePermission => rolePermission.Permission.Name)
                .ToListAsync();

            // This part seems excessive, all of it with caching
            _permissionsCache.TryAdd(userId, permissions);

            return permissions;
        }

        public async Task<IEnumerable<PermissionDto>> GetRolePermissionsAsync(string roleId, int organizationId)
        {
            Expression<Func<Permission, bool>> roleFilter = x => x.Roles.Any(y => y.Id == roleId);

            return await GetPermissionsAsync(organizationId, roleFilter);
        }

        private async Task<IEnumerable<PermissionDto>> GetPermissionsAsync(int organizationId, Expression<Func<Permission, bool>> roleFilter = null)
        {
            return await _permissionsDbSet
                .Include(x => x.Module.Organizations)
                .Include(x => x.Roles)
                .Where(roleFilter ?? (x => true))
                .Where(FilterActiveModules(organizationId))
                .Select(x => new PermissionDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Scope = x.Scope
                })
                .ToListAsync();
        }

        private static Expression<Func<Permission, bool>> FilterActiveModules(int organizationId)
        {
            return x => !x.ModuleId.HasValue || x.Module.Organizations.Any(y => y.Id == organizationId);
        }

        public void RemoveCache(string userId)
        {
            _permissionsCache.TryRemoveEntry(userId);
        }
    }
}