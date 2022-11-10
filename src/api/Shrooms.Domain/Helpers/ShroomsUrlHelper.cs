using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.Options;

namespace Shrooms.Domain.Helpers
{
    public static class ShroomsUrlHelper
    {
        public static string GetActionUrl(ApplicationOptions applicationOptions, ControllerRouteDto routeDto)
        {
            return $"{applicationOptions.ApiUrl}/{routeDto.ControllerName}/{routeDto.ActionName}";
        }
    }
}
