using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.Exceptions;
using Shrooms.Presentation.Api.Helpers;
using System.Security.Claims;

namespace Shrooms.Presentation.Api.Controllers
{
    // TODO: Refactor
    public class ShroomsControllerBase : ControllerBase
    {
        protected IActionResult BadRequestWithError(ValidationException ex)
        {
            return BadRequest(new { ErrorCode = ex.ErrorCode, ErrorMessage = ex.ErrorMessage });
        }

        protected UserAndOrganizationDto GetUserAndOrganization()
        {
            return User.Identity.GetUserAndOrganization();
        }

        protected UserAndOrganizationHubDto GetUserAndOrganizationHub()
        {
            return new UserAndOrganizationHubDto
            {
                OrganizationId = User.Identity.GetOrganizationId(),
                UserId = GetAuthenticatedUserId(),
                OrganizationName = User.Identity.GetOrganizationName()
            };
        }

        protected int GetOrganizationId()
        {
            return User.Identity.GetOrganizationId();
        }

        protected void SetOrganizationAndUser(UserAndOrganizationDto obj)
        {
            obj.OrganizationId = User.Identity.GetOrganizationId();
            obj.UserId = GetAuthenticatedUserId();
        }

        protected string GetAuthenticatedUserFullName()
        {
            return GetAuthenticatedUserClaim(ClaimTypes.GivenName);
        }

        protected string GetAuthenticatedUserId()
        {
            return GetAuthenticatedUserClaim(ClaimTypes.NameIdentifier);
        }

        protected string GetAuthenticatedUserEmail()
        {
            return GetAuthenticatedUserClaim(ClaimTypes.Email);
        }

        private string GetAuthenticatedUserClaim(string claimType)
        {
            if (HttpContext.User == null || 
                HttpContext.User.Identity == null ||
                !HttpContext.User.Identity.IsAuthenticated)
            {
                throw new ValidationException(ErrorCodes.UserNotFound, "User is not authenticated");
            }

            var claim = User.FindFirstValue(claimType);

            if (claim == null)
            {
                throw new ValidationException(ErrorCodes.InvalidClaim, $"Requested claim {claimType} does not exist");
            }

            return claim;
        }
    }
}