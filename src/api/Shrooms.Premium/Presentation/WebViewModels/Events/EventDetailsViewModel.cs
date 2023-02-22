﻿using Shrooms.Premium.Constants;
using System;
using System.Collections.Generic;

namespace Shrooms.Premium.Presentation.WebViewModels.Events
{
    public class EventDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime RegistrationDeadlineDate { get; set; }

        public bool AllowMaybeGoing { get; set; }
        public bool AllowNotGoing { get; set; }

        public IEnumerable<string> OfficesName { get; set; }

        public bool IsForAllOffices { get; set; }

        public bool IsPinned { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public int MaxVirtualParticipants { get; set; }

        public int MaxParticipants { get; set; }

        public int MaxOptions { get; set; }

        public string HostUserFullName { get; set; }

        public string HostUserId { get; set; }

        public bool IsFull { get; set; }

        public int WallId { get; set; }

        public IEnumerable<EventDetailsCommentViewModel> Comments { get; set; }

        public IEnumerable<EventDetailsOptionViewModel> Options { get; set; }

        public IEnumerable<EventParticipantViewModel> Participants { get; set; }

        public int GoingCount { get; set; }
        public int VirtuallyGoingCount { get; set; }
        public int MaybeGoingCount { get; set; }
        public int NotGoingCount { get; set; }

        public bool IsQueueAllowed { get; set; }

        public AttendingStatus ParticipatingStatus { get; set; }
        public bool IsInQueue { get; set; }
    }
}