using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Middlewares
{
    public abstract class ExceptionMiddlewareBase
    {
        protected readonly RequestDelegate _next;

        protected ExceptionMiddlewareBase(RequestDelegate next)
        {
            _next = next;
        }

        protected virtual async Task HandleExceptionAsync(HttpContext context, string errorMessage, int errorStatusCode, int? customStatusCode = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorStatusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                ErrorCode = customStatusCode ?? errorStatusCode,
                ErrorMessage = errorMessage
            }));
        }
    }
}
