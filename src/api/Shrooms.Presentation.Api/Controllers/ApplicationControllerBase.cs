using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Domain.Extensions;
using System;
using System.Security.Claims;

namespace Shrooms.Presentation.Api.Controllers
{
    public class ApplicationControllerBase : ControllerBase
    {
        protected UserAndOrganizationDto GetUserAndOrganization()
        {
            var claimsIdentity = GetAuthenticatedClaimsIdentity();
            return new UserAndOrganizationDto
            {
                OrganizationId = claimsIdentity.GetOrganizationId(),
                UserId = claimsIdentity.GetUserId()
            };
        }

        protected string GetUserId()
        {
            return GetAuthenticatedClaimsIdentity().GetUserId();
        }

        protected int GetOrganizationId()
        {
            return GetAuthenticatedClaimsIdentity().GetOrganizationId();
        }

        protected ControllerRouteDto GetControllerRoute(string? controllerName = null, string? actionName = null)
        {
            return new ControllerRouteDto
            {
                ControllerName = controllerName ?? ControllerContext.ActionDescriptor.ControllerName,
                ActionName = actionName ?? ControllerContext.ActionDescriptor.ActionName
            };
        }

        private ClaimsIdentity GetAuthenticatedClaimsIdentity()
        {
            if (User.Identity is ClaimsIdentity claimsIdentity && claimsIdentity.IsAuthenticated)
            {
                return claimsIdentity;
            }

            throw new InvalidOperationException(
                "Incorrect function usage. " +
                "Make sure that you are calling this method inside methods that use [Authorize] attribute");
        }
    }
}