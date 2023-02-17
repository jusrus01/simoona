using System.Collections.Generic;

namespace Shrooms.Premium.DataTransferObjects.Models.Events
{
    public class ClassifiedParticipantIdsDto
    {
        public List<string> NewParticipantIds { get; set; }
        public List<string> NewQueueParticipantIds { get; set; }
        public List<string> StatusChangeParticipantIds { get; set; }
        public List<string> StatusChangeQueueParticipantIds { get; set; }
    }
}
