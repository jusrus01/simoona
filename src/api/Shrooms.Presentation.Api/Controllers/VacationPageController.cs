using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.DataTransferObjects.VacationPages;
using Shrooms.Domain.Services.VacationPages;
using Shrooms.Presentation.WebViewModels.Models.VacationPage;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Controllers
{
    //[Authorize]
    [Route("VacationPage")]
    public class VacationPageController : ShroomsControllerBase
    {
        private readonly IVacationPageService _vacationPageService;
        private readonly IMapper _mapper;

        public VacationPageController(IVacationPageService vacationPageService, IMapper mapper)
        {
            _vacationPageService = vacationPageService;
            _mapper = mapper;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetVacationPage()
        {
            var vacationPageDto = await _vacationPageService.GetVacationPage(GetOrganizationId());

            if (vacationPageDto == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VacationPageDto, VacationPageViewModel>(vacationPageDto));
        }

        [HttpPut("Edit")]
        //[PermissionAuthorize(Permission = AdministrationPermissions.Vacation)]
        public async Task<IActionResult> EditVacationPage(VacationPageViewModel vacationPageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userAndOrg = GetUserAndOrganization();
            var vacationPageDto = _mapper.Map<VacationPageViewModel, VacationPageDto>(vacationPageViewModel);

            await _vacationPageService.EditVacationPage(userAndOrg, vacationPageDto);

            return Ok();
        }
    }
}