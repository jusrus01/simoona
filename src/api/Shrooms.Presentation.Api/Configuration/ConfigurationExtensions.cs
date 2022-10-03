using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class ConfigurationExtensions
    {
        public static void MapControllerRoutes(this IEndpointRouteBuilder endpoints)
        {
            // config.Routes.IgnoreRoute("elmah", "{resource}.axd/{*pathInfo}");

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            endpoints.Map("elmah/{resource}.axd/{*pathInfo}", async context =>
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
                context.Response.StatusCode = StatusCodes.Status404NotFound);

            // This will not be needed since we will handle requests with /storage in the middleware
            // config.Routes.IgnoreRoute("storage", "storage/{*pathInfo}"); 

//            config.Routes.MapHttpRoute(
//                name: "DefaultApi",
//                routeTemplate: "{controller}/{action}/{id}",
//                defaults: new { controller = "Default", action = "Index", id = RouteParameter.Optional });
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}", // TODO: Configure IIS so we would get api/
                defaults: new { controller = "Default", action = "Index" });

//            config.Routes.MapHttpRoute(
//                name: "Errors",
//                routeTemplate: "Error/{action}/",
//                defaults: new { action = "NotFound" });
            endpoints.MapControllerRoute(
                name: "errors",
                pattern: "error/{action}/",
                defaults: new { action = "NotFound" });
        }
    }
}
