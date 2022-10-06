using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.DAL;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Presentation.Api.Filters;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Controllers
{
    [AllowAnonymous]
    [SkipOrganizationValidationFilter]
    [Route("Default")]
    public class DefaultController : ControllerBase
    {
        private readonly IUnitOfWork2 _uow;

        public DefaultController(IUnitOfWork2 uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var count = await _uow.GetDbSet<ApplicationUser>().CountAsync();

            return Ok("API is up and running");
        }
    }
}