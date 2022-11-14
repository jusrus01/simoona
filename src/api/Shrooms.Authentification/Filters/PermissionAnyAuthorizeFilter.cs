using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Shrooms.Authentication.Filters
{
    public class PermissionAnyAuthorizeFilter : IAuthorizationFilter
    {
        private readonly List<string> _permissions;

        public PermissionAnyAuthorizeFilter(List<string> permissions)
        {
            _permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userClaims = context.HttpContext.User.Claims;

            foreach (var permission in _permissions)
            {
                if (userClaims.Any(claim => claim.Value == permission))
                {
                    return;
                }
            }
         
            context.Result = new ForbidResult();
        }
    }
}
