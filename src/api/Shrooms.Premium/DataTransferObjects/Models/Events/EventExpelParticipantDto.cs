namespace Shrooms.Premium.DataTransferObjects.Models.Events
{
    public class EventExpelParticipantDto
    {
        public EventParticipantDto RemovedParticipant { get; set; }
        public EventParticipantDto NextParticipant { get; set; }
    }
}
