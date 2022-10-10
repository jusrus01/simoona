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

        public IActionResult BadRequestWithError(ValidationException ex)
        {
            return BadRequest(new { ErrorCode = ex.ErrorCode, ErrorMessage = ex.ErrorMessage });
        }

        public UserAndOrganizationDto GetUserAndOrganization()
        {
            return User.Identity.GetUserAndOrganization();
        }

        public UserAndOrganizationHubDto GetUserAndOrganizationHub()
        {
            return new UserAndOrganizationHubDto
            {
                OrganizationId = User.Identity.GetOrganizationId(),
                UserId = GetUserId(),
                OrganizationName = User.Identity.GetOrganizationName()
            };
        }

        public int GetOrganizationId()
        {
            return User.Identity.GetOrganizationId();
        }

        public string GetOrganizationName()
        {
            return User.Identity.GetOrganizationName();
        }

        public void SetOrganizationAndUser(UserAndOrganizationDto obj)
        {
            obj.OrganizationId = User.Identity.GetOrganizationId();
            obj.UserId = GetUserId();
        }

        public string GetUserFullName()
        {
            return User.FindFirstValue(ClaimTypes.GivenName);
        }

        public string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string GetUserEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email);
        }
    }
}