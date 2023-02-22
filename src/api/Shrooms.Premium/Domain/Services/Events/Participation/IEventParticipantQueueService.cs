using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.DataLayer.EntityModels.Models.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shrooms.Premium.Domain.Services.Events.Participation
{
    public interface IEventParticipantQueueService
    {
        IEnumerable<EventParticipant> UpdateQueue(Event @event);

        Task ClearAllQueuesFromOrganizationAsync(int organizationId);

        Task ClearQueueFromEventAsync(Event @event);
    }
}
