using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.DataLayer.EntityModels.Models.Events;
using System;
using System.Threading.Tasks;

namespace Shrooms.Premium.Domain.Services.Events.Participation
{
    public interface IEventParticipantQueueService
    {
        void UpdateQueue(Event @event);

        Task ClearAllQueuesFromOrganizationAsync(Organization organization);
    }
}
