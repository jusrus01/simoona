using System;
using System.Collections.Generic;
using System.Linq;
using Shrooms.Contracts.Enums;

namespace Shrooms.DataLayer.EntityModels.Models.Events
{
    public class EventOption : BaseModel
    {
        public Guid EventId { get; set; }
        
        public virtual Event Event { get; set; }
        
        public string Option { get; set; }
        
        public OptionRules Rule { get; set; }

        public IEnumerable<EventParticipant> EventParticipants
        {
            get => EventParticipantEventOptions.Select(model => model.EventParticipant);
        }

        // Required for many-to-many
        public ICollection<EventParticipantEventOption> EventParticipantEventOptions { get; set; }
    }
}
