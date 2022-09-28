using System;
using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.ModelsCore.Events
{
    public class EventParticipant : BaseModel
    {
        public Guid EventId { get; set; }
        
        public virtual Event Event { get; set; }
        
        public string ApplicationUserId { get; set; }
        
        public int AttendStatus { get; set; }
        
        public string AttendComment { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        public IEnumerable<EventOption> EventOptions
        {
            get => EventParticipantEventOptions.Select(model => model.EventOption);
        }

        // Required for many-to-many
        internal ICollection<EventParticipantEventOption> EventParticipantEventOptions { get; set; }
    }
}
