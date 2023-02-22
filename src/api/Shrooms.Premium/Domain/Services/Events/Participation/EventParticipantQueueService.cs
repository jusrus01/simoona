using Newtonsoft.Json.Linq;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.Infrastructure;
using Shrooms.DataLayer.EntityModels.Models.Events;
using Shrooms.Domain.Services.Wall;
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
        private readonly IWallService _wallService;

        private readonly DbSet<Event> _eventsDbSet;
        private readonly DbSet<EventParticipant> _eventParticipantsDbSet;

        public EventParticipantQueueService(
            IUnitOfWork2 uow,
            ISystemClock systemClock,
            IWallService wallService)
        {
            _uow = uow;
            _wallService = wallService;
            _systemClock = systemClock;

            _eventsDbSet = uow.GetDbSet<Event>();
            _eventParticipantsDbSet = uow.GetDbSet<EventParticipant>();
        }

        public async Task ClearAllQueuesFromOrganizationAsync(int organizationId)
        {
            var events = await _eventsDbSet
                .Include(e => e.EventParticipants)
                .Where(e =>
                    e.OrganizationId == organizationId &&
                    e.EventParticipants.Any(participant => participant.IsInQueue))
                .ToListAsync();

            var startedEvents = events
                .Where(e => e.StartDate < _systemClock.UtcNow)
                .ToList();
            foreach (var @event in startedEvents)
            {
                await ClearQueueFromEventInternalAsync(@event);
            }

            await _uow.SaveChangesAsync(false);
        }

        public async Task ClearQueueFromEventAsync(Event @event)
        {
            await ClearQueueFromEventInternalAsync(@event);
        }

        /// <summary>
        /// Handles queue state for <paramref name="@event"/>
        /// </summary>
        /// <param name="event">Event</param>
        /// <returns>Participants that were moved from the queue to participant list</returns>
        public IEnumerable<EventParticipant> UpdateQueue(Event @event)
        {
            return MoveFromQueueToAvailableSpace(@event, AttendingStatus.Attending)
                .Union(MoveFromQueueToAvailableSpace(@event, AttendingStatus.AttendingVirtually));
        }

        private async Task ClearQueueFromEventInternalAsync(Event @event)
        {
            var participantsToRemove = @event.EventParticipants
                .Where(p => p.IsInQueue)
                .ToList();

            foreach (var participant in participantsToRemove)
            {
                await _wallService.JoinOrLeaveWallAsync(
                    @event.WallId,
                    participant.ApplicationUserId,
                    participant.ApplicationUserId,
                    @event.OrganizationId.Value,
                    isEventWall: true);

                _eventParticipantsDbSet.Remove(participant);
            }
        }

        private static List<EventParticipant> MoveFromQueueToAvailableSpace(Event @event, AttendingStatus status)
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
            return addParticipants;
        }
    }
}
