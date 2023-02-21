﻿using System;
using System.Collections.Generic;
using Shrooms.DataLayer.EntityModels.Models.Events;

namespace Shrooms.Premium.Presentation.WebViewModels.Events
{
    public class EventEditDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? RegistrationDeadlineDate { get; set; }

        public EventRecurrenceOptions Recurrence { get; set; }

        public bool AllowMaybeGoing { get; set; }

        public bool AllowNotGoing { get; set; }

        public IEnumerable<int> OfficeIds { get; set; }

        public bool IsPinned { get; set; }

        public string Location { get; set; }

        public int TypeId { get; set; }

        public string Description { get; set; }

        public int MaxParticipants { get; set; }

        public int MaxVirtualParticipants { get; set; }

        public int MaxOptions { get; set; }

        public string HostUserId { get; set; }

        public string HostUserFullName { get; set; }

        public bool ResetParticipantList { get; set; }

        public bool IsShownInUpcomingEventsWidget { get; set; }

        public IEnumerable<EventOptionViewModel> Options { get; set; }

        public IEnumerable<EventReminderDetailsViewModel> Reminders { get; set; }
        
        public bool IsQueueAllowed { get; set; }
    }
}
