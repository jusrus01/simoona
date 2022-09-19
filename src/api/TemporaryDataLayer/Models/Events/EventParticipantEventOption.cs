namespace TemporaryDataLayer.Models.Events
{
    public class EventParticipantEventOption
    {
        public int EventParticipantId { get; set; }

        public EventParticipant EventParticipant { get; set; }

        public int EventOptionId { get; set; }

        public EventOption EventOption { get; set; }
    }
}
