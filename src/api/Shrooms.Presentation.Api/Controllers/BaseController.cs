using System.Security.Claims;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.Exceptions;
using Shrooms.Presentation.Api.Helpers;
using Microsoft.AspNetCore.Http;
using System;

namespace Shrooms.Presentation.Api.Controllers
{
    public class BaseController : ApiController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        //public BaseController(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}

        public StatusCodeResult Forbidden()
        {
            return StatusCode(HttpStatusCode.Forbidden);
        }

        public StatusCodeResult UnsupportedMediaType()
        {
            return StatusCode(HttpStatusCode.UnsupportedMediaType);
        }

        public IHttpActionResult BadRequestWithError(ValidationException ex)
        {
            return Content(HttpStatusCode.BadRequest, new { ErrorCode = ex.ErrorCode, ErrorMessage = ex.ErrorMessage });
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
                //UserId = User.Identity.GetUserId(),
                //UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
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
            return GetClaimTypeValueFromUser(ClaimTypes.GivenName);
        }

        public string GetUserId()
        {
            return GetClaimTypeValueFromUser(ClaimTypes.NameIdentifier);
        }

        public string GetUserEmail()
        {
            return GetClaimTypeValueFromUser(ClaimTypes.Email);
        }

        private string GetClaimTypeValueFromUser(string claimTypeName)
        {
            var user = GetCurrentRequestUser();

            if (user == null)
            {
                return string.Empty;
            }

            return user.FindFirst(claimTypeName).Value;
        }

        private ClaimsPrincipal GetCurrentRequestUser()
        {
            if (_httpContextAccessor == null)
            {
                throw new Exception($"{nameof(IHttpContextAccessor)} not injected into {nameof(BaseController)}");
            }

            return _httpContextAccessor.HttpContext.User;
        }
    }
}