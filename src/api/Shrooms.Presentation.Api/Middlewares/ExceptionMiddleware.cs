using Microsoft.AspNetCore.Http;
using Shrooms.Contracts.Exceptions;
using Shrooms.Domain.Exceptions.Exceptions;
using System;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Middlewares
{
    public class ExceptionMiddleware : ExceptionMiddlewareBase
    {
        public ExceptionMiddleware(RequestDelegate next)
            :
            base(next)
        {
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
            catch
            {
                await HandleExceptionAsync(httpContext, "Internal server error", StatusCodes.Status500InternalServerError);
            }
        }
    }
}
