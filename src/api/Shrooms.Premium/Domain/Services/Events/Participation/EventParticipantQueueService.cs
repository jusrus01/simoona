using Shrooms.Contracts.DAL;
using Shrooms.Contracts.Infrastructure;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.DataLayer.EntityModels.Models.Events;
using Shrooms.Premium.Constants;
using Shrooms.Premium.Domain.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Shrooms.Premium.Domain.Services.Events.Participation
{
    public class EventParticipantQueueService : IEventParticipantQueueService
    {
        private readonly IUnitOfWork2 _uow;
        private readonly ISystemClock _systemClock;

        private readonly DbSet<Event> _eventsDbSet;

        public EventParticipantQueueService(IUnitOfWork2 uow, ISystemClock systemClock)
        {
            _uow = uow;
            _systemClock = systemClock;

            _eventsDbSet = uow.GetDbSet<Event>();
        }

        public async Task ClearAllQueuesFromOrganizationAsync(Organization organization)
        {
            var events = await _eventsDbSet
                .Include(e => e.EventParticipants)
                .Where(e =>
                    e.OrganizationId == organization.Id &&
                    e.EventParticipants.Any(participant => participant.IsInQueue))
                .ToListAsync();

            var startedEvents = events
                .Where(e => e.StartDate < _systemClock.UtcNow)
                .ToList();
            ClearEventQueues(startedEvents);

            await _uow.SaveChangesAsync(false);
        }

        public void UpdateQueue(Event @event)
        {
            RemoveParticipantsFromQueue(@event, AttendingStatus.Attending);
            RemoveParticipantsFromQueue(@event, AttendingStatus.AttendingVirtually);
        }

        private static void RemoveParticipantsFromQueue(Event @event, AttendingStatus status)
        {
            var availableSpaceCount = @event.GetMaxParticipantCount(status) -
                @event.EventParticipants
                    .Count(p =>
                        !p.IsInQueue &&
                        p.AttendStatus == (int)status);
            var addParticipants = @event.EventParticipants
                .Where(p =>
                    p.IsInQueue &&
                    p.AttendStatus == (int)status)
                .Take(availableSpaceCount)
                .ToList();

            foreach (var participant in addParticipants)
            {
                participant.IsInQueue = false;
            }
        }

        private static void ClearEventQueues(List<Event> startedEvents)
        {
            var participantsToClear = startedEvents
                .SelectMany(e => e.EventParticipants.Where(participant => participant.IsInQueue))
                .ToList();
            foreach (var participant in participantsToClear)
            {
                participant.AttendStatus = (int)AttendingStatus.Idle;
                participant.IsInQueue = false;
            }
        }
    }
}
