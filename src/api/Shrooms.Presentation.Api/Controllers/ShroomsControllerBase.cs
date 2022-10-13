using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.Exceptions;
using Shrooms.Presentation.Api.Helpers;
using System.Security.Claims;

namespace Shrooms.Presentation.Api.Controllers
{
    public class ShroomsControllerBase : ControllerBase
    {
        //public StatusCodeResult Forbidden()
        //{
        //    return StatusCode(HttpStatusCode.Forbidden);
        //}

        //public StatusCodeResult UnsupportedMediaType()
        //{
        //    return StatusCode(HttpStatusCode.UnsupportedMediaType);
        //}

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
                UserId = GetUserId(),
                OrganizationName = User.Identity.GetOrganizationName()
            };
        }

        protected int GetOrganizationId()
        {
            return User.Identity.GetOrganizationId();
        }

        protected string GetOrganizationName()
        {
            return User.Identity.GetOrganizationName();
        }

        protected void SetOrganizationAndUser(UserAndOrganizationDto obj)
        {
            obj.OrganizationId = User.Identity.GetOrganizationId();
            obj.UserId = GetUserId();
        }

        protected string GetUserFullName()
        {
            return User.FindFirstValue(ClaimTypes.GivenName);
        }

        protected string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        protected string GetUserEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email);
        }
    }
}