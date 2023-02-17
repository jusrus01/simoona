using Shrooms.Contracts.DataTransferObjects;
using Shrooms.Premium.Constants;
using System;

namespace Shrooms.Premium.Domain.Extensions
{
    public static class EventCapacityExtensions
    {
        public static int GetMaxParticipantCount(this IEventCapacity capacity, AttendingStatus status)
        {
            if (status == AttendingStatus.Attending)
            {
                return capacity.MaxParticipants;
            }
            else if (status == AttendingStatus.AttendingVirtually)
            {
                return capacity.MaxVirtualParticipants;
            }

            throw new NotSupportedException($"This function cannot be used with {status} status");
        }
    }
}
