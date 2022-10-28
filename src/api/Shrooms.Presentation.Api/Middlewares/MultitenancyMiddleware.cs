using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;
using Shrooms.Contracts.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Middlewares
{
    public class MultiTenancyMiddleware : ExceptionMiddlewareBase
    {
        private readonly ApplicationOptions _applicationOptions;

        public MultiTenancyMiddleware(RequestDelegate next, IOptions<ApplicationOptions> applicationOptions)
            :
            base(next)
        {
            _applicationOptions = applicationOptions.Value;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                SetRequestOrganizationHeader(httpContext);
                
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(httpContext, ex.ErrorMessage, StatusCodes.Status400BadRequest, ex.ErrorCode);
            }
        }

        private void SetRequestOrganizationHeader(HttpContext httpContext)
        {
            var tenantName = ExtractTenantName(httpContext);

            if (tenantName == null)
            {
                throw new ValidationException(ErrorCodes.Unspecified, "Organization not provided");
            }
            else if (!_applicationOptions.ConnectionStrings.ContainsKey(tenantName))
            {
                throw new ValidationException(ErrorCodes.InvalidOrganization, "Invalid organization");
            }
            else
            {
                httpContext.Request.Headers[WebApiConstants.OrganizationHeader] = tenantName;
            }
        }

        private static string? ExtractTenantName(HttpContext httpContext)
        {
            if (httpContext.User != null &&
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                httpContext.User.Identity.IsAuthenticated &&
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                httpContext.User.Claims.Any(x => x.Type == WebApiConstants.ClaimOrganizationName))
            {
                return httpContext.User.Claims.First(x => x.Type == WebApiConstants.ClaimOrganizationName).Value;
            }

            // TODO: Change this to case-insensitive
            if (httpContext.Request.Headers.TryGetValue("Organization", out var organizationFromHeader))
            {
                return organizationFromHeader;
            }

            if (httpContext.Request.Query.TryGetValue("organization", out var organizationFromUri))
            {
                return organizationFromUri;
            }

            if (httpContext.Request.Path.ToString().StartsWith("/storage"))
            {
                return httpContext.Request.Path.ToString().Split('/')[2];
            }

            return null;
        }
    }
}