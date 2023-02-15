﻿using System;
using System.Collections.Generic;

namespace Shrooms.DataLayer.EntityModels.Models.Events
{
    public class EventParticipant : BaseModel
    {
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }
        public string ApplicationUserId { get; set; }
        public int AttendStatus { get; set; }
        public bool IsInQueue { get; set; }
        public string AttendComment { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<EventOption> EventOptions { get; set; }
    }
}
