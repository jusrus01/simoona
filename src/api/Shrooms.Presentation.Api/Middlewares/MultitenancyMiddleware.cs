﻿//using System;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Owin;
//using Shrooms.Contracts.Constants;
//using Shrooms.Presentation.Api.GeneralCode;
//using Shrooms.Resources.Helpers;

//namespace Shrooms.Presentation.Api.Middlewares
//{
//    public class MultiTenancyMiddleware : OwinMiddleware
//    {
//        public MultiTenancyMiddleware(OwinMiddleware next)
//            : base(next)
//        {
//        }

//        // TODO
//        public override async Task Invoke(IOwinContext context)
//        {
//            var request = context.Request;

//            if (request.Path.ToString().StartsWith("/signin-google") || request.Path.ToString().StartsWith("/signin-facebook") || request.Path.ToString().StartsWith("/swagger"))
//            {
//                await Next.Invoke(context);
//            }
//            else
//            {
//                await Next.Invoke(context);
//                //var tenantKey = ExtractTenant(context);

//                //if (string.IsNullOrEmpty(tenantKey))
//                //{
//                //    Unauthorized(context);
//                //}
//                //else if (!TryFindTenant(out var tenantName, tenantKey))
//                //{
//                //    await ReturnInvalidOrganizationResponse(context);
//                //}
//                //else
//                //{
//                //    request.Set("tenantName", tenantName);

//                //    try
//                //    {
//                //        await Next.Invoke(context);
//                //    }
//                //    catch (OperationCanceledException)
//                //    {
//                //    }
//                //}
//            }
//        }

//        private static string ExtractTenant(IOwinContext context)
//        {
//            var tenantKey = default(string);
//            var requestPath = context.Request.Path.ToString();

//            if (requestPath.StartsWith("/storage"))
//            {
//                tenantKey = context.Request.Path.ToString().Split('/')[2];
//            }
//            else if (context.Authentication.User != null &&
//                context.Authentication.User.Identity.IsAuthenticated &&
//                context.Authentication.User.Claims.Any(x => x.Type == "OrganizationName"))
//            {
//                tenantKey = context.Authentication.User.Claims.First(x => x.Type == "OrganizationName").Value.ToLowerInvariant();
//            }
//            else if (requestPath.StartsWith("/token") ||
//                requestPath.StartsWith("/externaljobs") ||
//                requestPath.StartsWith("/externalpremiumjobs") ||
//                requestPath.StartsWith("/Account/ExternalLogin") ||
//                requestPath.StartsWith("/Account/RegisterExternal") ||
//                requestPath.StartsWith("/Account/InternalLogins") ||
//                requestPath.StartsWith("/Account/UserInfo") ||
//                requestPath.StartsWith("/Account/Register") ||
//                requestPath.StartsWith("/Account/ResetPassword") ||
//                requestPath.StartsWith("/Account/RequestPasswordReset") ||
//                requestPath.StartsWith("/Account/VerifyEmail") ||
//                requestPath.StartsWith("/bookmobile"))
//            {
//                var organizationFromHeader = context.Request.Headers.Get("Organization");
//                var organizationFromUri = context.Request.Query.Get("organization");

//                if (organizationFromHeader != null)
//                {
//                    tenantKey = organizationFromHeader;
//                }
//                else if (organizationFromUri != null)
//                {
//                    tenantKey = organizationFromUri;
//                }
//            }

//             return tenantKey;
//        }

//        private static async Task ReturnInvalidOrganizationResponse(IOwinContext context)
//        {
//            context.Response.StatusCode = 400;
//            context.Response.ReasonPhrase = "Invalid organization";
//            context.Response.ContentType = "application/json";
//            var responseBody = new
//            {
//                errorCode = ErrorCodes.InvalidOrganization,
//                errorMessage = "Invalid organization"
//            };

//            await context.Response.WriteAsync(Encoding.UTF8.GetBytes(responseBody.ToJson()));
//        }

//        private static void Unauthorized(IOwinContext context)
//        {
//            context.Response.StatusCode = 401;
//            context.Response.ReasonPhrase = "Unauthorized";
//        }

//        private static bool TryFindTenant(out string tenantName, string tenantKey)
//        {
//            var tenantMatch = OrganizationUtils.AvailableOrganizations.TryGetValue(tenantKey, out tenantName);
//            return tenantMatch;
//        }
//    }
//}