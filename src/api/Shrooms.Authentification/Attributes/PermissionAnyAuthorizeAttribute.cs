using Microsoft.AspNetCore.Mvc;
using Shrooms.Authentication.Filters;
using System.Linq;

namespace Shrooms.Authentication.Attributes
{
    public class PermissionAnyAuthorizeAttribute : TypeFilterAttribute
    {
        public PermissionAnyAuthorizeAttribute(params string[] permissions) : base(typeof(PermissionAnyAuthorizeFilter))
        {
            Arguments = new object[] { permissions.ToList() };
        }
    }
}
