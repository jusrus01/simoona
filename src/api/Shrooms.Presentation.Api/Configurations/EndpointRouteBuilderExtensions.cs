using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapControllerRoutes(this IEndpointRouteBuilder endpoints)
        {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            endpoints.Map("elmah/{resource}.axd/{*pathInfo}", async context =>
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
                context.Response.StatusCode = StatusCodes.Status404NotFound);
            
            endpoints.MapControllerRoute(
                name: "DefaultApi",
                pattern: "{controller}/{action}/{id?}",
                defaults: new { controller = "Default", action = "Index" });

            endpoints.MapControllerRoute(
                name: "errors",
                pattern: "error/{action}/",
                defaults: new { action = "NotFound" });
        }
    }
}