using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Authentication.Attributes;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Notification;
using Shrooms.Contracts.Enums;
using Shrooms.Contracts.ViewModels.Notifications;
using Shrooms.Domain.Services.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Controllers
{
    [Route("Notification")]
    [Authorize]
    public class NotificationController : ShroomsControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _notificationService.GetAllAsync(GetUserAndOrganization());

            return Ok(MakeCommentsStacked(result));
        }

        private IEnumerable<NotificationViewModel> MakeCommentsStacked(IEnumerable<NotificationDto> comments)
        {
            var stackedList = new List<NotificationViewModel>();

            foreach (var item in comments)
            {
                var parentComment = stackedList
                    .FirstOrDefault(x => CompareSourcesIds(x.sourceIds, item.SourceIds) && item.Type != NotificationType.EventReminder);

                if (parentComment == null)
                {
                    stackedList.Add(_mapper.Map<NotificationViewModel>(item));
                }
                else
                {
                    parentComment.stackedIds.Add(item.Id);
                    if (parentComment.title.Equals(item.Title) == false)
                    {
                        parentComment.others++;
                    }
                }
            }

            return stackedList;
        }

        private static bool CompareSourcesIds(SourcesViewModel viewModel, SourcesDto dtoModel)
        {
            if (viewModel.PostId != dtoModel.PostId ||
                viewModel.EventId != dtoModel.EventId ||
                viewModel.ProjectId != dtoModel.ProjectId ||
                viewModel.WallId != dtoModel.WallId)
            {
                return false;
            }

            return true;
        }

        [HttpPut("MarkAsRead")]
        public async Task<IActionResult> MarkAsRead(IEnumerable<int> ids)
        {
            await _notificationService.MarkAsReadAsync(GetUserAndOrganization(), ids);

            return Ok();
        }

        [HttpPut("MarkAllAsRead")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            await _notificationService.MarkAllAsReadAsync(GetUserAndOrganization());

            return Ok();
        }

    }
}