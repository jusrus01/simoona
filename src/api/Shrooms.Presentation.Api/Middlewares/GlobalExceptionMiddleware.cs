using Microsoft.AspNetCore.Http;
using Shrooms.Contracts.Exceptions;
using Shrooms.Domain.Exceptions.Exceptions;
using System;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(httpContext, ex.ErrorMessage, StatusCodes.Status400BadRequest, ex.ErrorCode);
            }
            catch (UnauthorizedException)
            {
                await HandleExceptionAsync(httpContext, errorMessage: "Unauthorized", StatusCodes.Status401Unauthorized);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, string errorMessage, int errorStatusCode, int? customStatusCode = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorStatusCode;

            var errorJsonResponse = $"{{ \"StatusCode\":\"{customStatusCode ?? errorStatusCode}\", \"Message\":\"{errorMessage}\" }}";

            await context.Response.WriteAsync(errorJsonResponse);
        }
    }
}
