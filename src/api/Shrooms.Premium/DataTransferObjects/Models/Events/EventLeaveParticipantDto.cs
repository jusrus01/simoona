namespace Shrooms.Premium.DataTransferObjects.Models.Events
{
    public class EventLeaveParticipantDto
    {
        public EventParticipantDto RemovedParticipant { get; set; }
        public EventParticipantDto NextParticipant { get; set; }
    }
}
