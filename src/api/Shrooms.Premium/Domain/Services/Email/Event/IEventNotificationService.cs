﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Contracts.DataTransferObjects.Events;
using Shrooms.Contracts.DataTransferObjects.Users;
using Shrooms.Contracts.Infrastructure.Email;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Premium.DataTransferObjects.Models.Events;
using Shrooms.Premium.DataTransferObjects.Models.Events.Reminders;

namespace Shrooms.Premium.Domain.Services.Email.Event
{
    public interface IEventNotificationService
    {
        Task NotifyRemovedEventParticipantsAsync(string eventName, Guid eventId, int orgId, IEnumerable<string> users);

        Task RemindUsersToJoinEventAsync(IEnumerable<EventTypeDto> eventType, IEnumerable<string> users, int orgId);

        Task NotifyManagerAboutEventAsync(UserEventAttendStatusChangeEmailDto userAttendStatusDto, bool isJoiningEvent);

        Task RemindUsersAboutDeadlineDateOfJoinedEventsAsync(IEnumerable<EventReminderDeadlineEmailDto> deadlineEmailDtos, Organization organization);

        Task RemindUsersAboutStartDateOfJoinedEventsAsync(IEnumerable<EventReminderStartEmailDto> startEmailDtos, Organization organization);
        
        Task NotifySharedEventAsync(SharedEventEmailDto shareEventEmailDto, UserAndOrganizationHubDto userOrgHubDto);

        Task NotifyNewEventAsync(CreateEventDto eventArgsDto, IEnumerable<IEmailReceiver> receivers, UserAndOrganizationHubDto userOrgHubDto);
    }
}
