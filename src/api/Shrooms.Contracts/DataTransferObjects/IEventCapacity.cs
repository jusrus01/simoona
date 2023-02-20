namespace Shrooms.Contracts.DataTransferObjects
{
    public interface IEventCapacity
    {
        int MaxParticipants { get; set; }

        int MaxVirtualParticipants { get; set; }
    }
}
