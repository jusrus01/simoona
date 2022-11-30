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
                SetCurrentRequestOrganizationHeader(httpContext);

                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(httpContext, ex.ErrorMessage, StatusCodes.Status400BadRequest, ex.ErrorCode);
            }
        }

        private void SetCurrentRequestOrganizationHeader(HttpContext httpContext)
        {
            var tenantName = ExtractTenantName(httpContext);
            ValidateTenant(tenantName);
#pragma warning disable CS8604 // Possible null reference argument.
            SetOrganizationHeader(httpContext, tenantName);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        private static void SetOrganizationHeader(HttpContext httpContext, string tenantName)
        {
            httpContext.Request.Headers[WebApiConstants.OrganizationHeader] = tenantName;
        }

        private static string? ExtractTenantName(HttpContext httpContext)
        {
            if (IsCallerAuthenticated(httpContext))
            {
                return httpContext.User.Claims.First(x => x.Type == WebApiConstants.ClaimOrganizationName).Value;
            }

            if (httpContext.Request.Headers.TryGetValue(WebApiConstants.OrganizationHeader, out var organizationFromHeader))
            {
                return organizationFromHeader;
            }

            if (httpContext.Request.Query.TryGetValue(WebApiConstants.OrganizationHeader, out var organizationFromUri))
            {
                return organizationFromUri;
            }

            // TODO: Before deployment configure hosting service to
            // allow StorageController to handle image retrieval.

            return null;
        }

        private static bool IsCallerAuthenticated(HttpContext httpContext)
        {
            return httpContext.User != null &&
                httpContext.User.Identity != null &&
                httpContext.User.Identity.IsAuthenticated;
        }

        private void ValidateTenant(string? tenantName)
        {
            CheckIfTenantExists(tenantName);
#pragma warning disable CS8604 // Possible null reference argument.
            CheckIfIsValidTenant(tenantName);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        private static void CheckIfTenantExists(string? tenantName)
        {
            if (string.IsNullOrEmpty(tenantName))
            {
                throw new ValidationException(ErrorCodes.OrganizationNotProvided, "Organization not provided");
            }
        }

        private void CheckIfIsValidTenant(string tenantName)
        {
            if (!_applicationOptions.ConnectionStrings.ContainsKey(tenantName))
            {
                throw new ValidationException(ErrorCodes.InvalidOrganization, "Invalid organization");
            }
        }
    }
}