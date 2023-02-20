﻿using Shrooms.DataLayer.EntityModels.Models.Events;
using Shrooms.Premium.Constants;
using Shrooms.Premium.Presentation.WebViewModels.ValidationAttributes.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shrooms.Premium.Presentation.WebViewModels.Events
{
    public class UpdateEventViewModel
    {
        [Required]
        public string Id { get; set; }

        public bool ResetParticipantList { get; set; }

        public bool ResetVirtualParticipantList { get; set; }

        public IEnumerable<EventOptionViewModel> EditedOptions { get; set; }

        [Required]
        [StringLength(EventsConstants.EventNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }


        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public DateTime? RegistrationDeadlineDate { get; set; }

        [Required]
        public EventRecurrenceOptions? Recurrence { get; set; }

        public bool AllowMaybeGoing { get; set; }
        public bool AllowNotGoing { get; set; }

        public bool IsShownInUpcomingEventsWidget { get; set; }

        [Required]
        public List<int> Offices { get; set; }

        public bool IsPinned { get; set; }

        [Required]
        [StringLength(EventsConstants.EventLocationMaxLength)]
        public string Location { get; set; }

        [StringLength(EventsConstants.EventDescriptionMaxLength)]
        public string Description { get; set; }

        [Range(EventsConstants.EventMinimumParticipants, EventsConstants.EventMaxParticipants)]
        public int MaxParticipants { get; set; }

        [Range(EventsConstants.EventMinimumParticipants, EventsConstants.EventMaxParticipants)]
        public int MaxVirtualParticipants { get; set; }

        [Range(EventsConstants.EventMinimumOptions, short.MaxValue)]
        public int MaxOptions { get; set; }

        public int TypeId { get; set; }

        [Required]
        public string ResponsibleUserId { get; set; }

        public IEnumerable<NewEventOptionViewModel> NewOptions { get; set; }

        [RequireOneTimeEventForCollection(nameof(Recurrence)), ValidateRemindersCollection]
        public IEnumerable<EventReminderViewModel> Reminders { get; set; }

        public bool IsQueueAllowed { get; set; }
    }
}
