using Microsoft.AspNetCore.Mvc;
using Shrooms.Authentication.Filters;
using System.Linq;

namespace Shrooms.Authentication.Attributes
{
    public class PermissionAuthorizeAttribute : TypeFilterAttribute
    {
        public PermissionAuthorizeAttribute(params string[] permissions) : base(typeof(PermissionAuthorizeFilter))
        {
            Arguments = new object[] { permissions.ToList() };
        }
    }
}
