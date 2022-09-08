﻿using System.Collections.Generic;

namespace TemporaryDataLayer
{
    public class EventType : BaseModelWithOrg
    {
        public string Name { get; set; }

        public bool IsSingleJoin { get; set; }

        public string SingleJoinGroupName { get; set; }

        public bool SendWeeklyReminders { get; set; }

        public bool IsShownWithMainEvents { get; set; }

        public bool SendEmailToManager { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
