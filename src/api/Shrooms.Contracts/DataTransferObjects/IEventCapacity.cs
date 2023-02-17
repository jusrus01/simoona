namespace Shrooms.Contracts.DataTransferObjects
{
    public interface IEventCapacity
    {
        public int MaxParticipants { get; set; }

        public int MaxVirtualParticipants { get; set; }
    }
}
