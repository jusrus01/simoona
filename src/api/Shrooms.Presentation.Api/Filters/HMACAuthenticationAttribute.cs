﻿//using System;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http.Filters;
//using System.Web.Http.Results;
//using Shrooms.Contracts.DAL;
//using Shrooms.DataLayer.EntityModels.Models;

//namespace Shrooms.Presentation.Api.Filters
//{
//    public class HmacAuthenticationAttribute : Attribute, IAuthenticationFilter
//    {
//        private const string OrganizationHeaderName = "Organization";

//        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
//        {
//            var authorizationGuid = GetAuthorizationGuid(context);

//            if (authorizationGuid == null)
//            {
//                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
//                return Task.FromResult(0);
//            }

//            if (context.Request.Headers.Authorization == null)
//            {
//                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
//                return Task.FromResult(0);
//            }

//            if (context.Request.Headers.Authorization.Parameter != authorizationGuid)
//            {
//                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
//            }

//            return Task.FromResult(0);
//        }

//        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
//        {
//            return Task.FromResult(0);
//        }

//        public bool AllowMultiple => false;

//        private string GetAuthorizationGuid(HttpAuthenticationContext context)
//        {
//            var unitOfWork = context.ActionContext.Request.GetDependencyScope().GetService(typeof(IUnitOfWork2)) as IUnitOfWork2;

//            if (!context.Request.Headers.Contains(OrganizationHeaderName))
//            {
//                return null;
//            }

//            var organizationName = context.Request.Headers.GetValues(OrganizationHeaderName).FirstOrDefault();

//            return unitOfWork.GetDbSet<Organization>()
//                .Where(x => x.ShortName == organizationName)
//                .Select(x => x.BookAppAuthorizationGuid)
//                .FirstOrDefault();
//        }
//    }
//}