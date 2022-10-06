using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Shrooms.Presentation.Api.Controllers
{
    [AllowAnonymous]
    public class ErrorController : ControllerBase
    {
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions]
        public IActionResult NotFound(string path)
        {
            //LogManager.GetCurrentClassLogger().Log(
            //   LogLevel.Info,
            //   new HttpException((int)HttpStatusCode.NotFound, $"404 Not Found: /{path}"));

            // TODO: ask about this
            // TODO: implement better :)
            LogManager.GetCurrentClassLogger().Log(
                LogLevel.Info, 
                new Exception($"404 Not Found: /{path}"));

            return NotFound();
        }
    }
}