using NSubstitute;
using NUnit.Framework;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.Infrastructure;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.DataLayer.EntityModels.Models.Events;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Wall;
using Shrooms.Premium.Constants;
using Shrooms.Premium.Domain.Services.WebHookCallbacks.Events;
using Shrooms.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Shrooms.Premium.Tests.DomainService.WebHookCallbacks
{
    [TestFixture]
    public class EventsWebHookServiceTests
    {
        private const int DefaultOrganizationId = 1;
        private const string DefaultOrganizationName = "test";

        private ISystemClock _systemClock;
        private IUnitOfWork2 _uow;
        private IOrganizationService _organizationService;

        private DbSet<Event> _eventsDbSet;

        private EventsWebHookService _sut;

        [SetUp]
        public void TestInitializer()
        {
            _uow = Substitute.For<IUnitOfWork2>();
            _eventsDbSet = _uow.MockDbSetForAsync<Event>();
            _uow.MockDbSetForAsync<EventOption>();

            _systemClock = Substitute.For<ISystemClock>();
            _systemClock.UtcNow.Returns(DateTime.UtcNow);
            _organizationService = Substitute.For<IOrganizationService>();

            _sut = new EventsWebHookService(
                _uow,
                _systemClock,
                Substitute.For<IWallService>(),
                Substitute.For<IApplicationSettings>(),
                _organizationService);
        }

        [Test]
        public async Task Should_Remove_One_Participant_From_Virtual_Queue()
        {
            // Arrange
            var @event = MockEventForQueueUpdateTests(
                status: AttendingStatus.AttendingVirtually,
                eventStartDate: DateTime.UtcNow.AddDays(10),
                maxVirtualParticipants: 4,
                maxParticipants: 0,
                participantCount: 5,
                addToQueueCount: 2);

            // Act
            await _sut.UpdateEventQueues(DefaultOrganizationName);

            // Assert
            Assert.AreEqual(1, @event.EventParticipants.Count(p => p.IsInQueue));
        }

        [Test]
        public async Task Should_Remove_All_Participants_From_Virtual_Queue()
        {
            // Arrange
            var @event = MockEventForQueueUpdateTests(
                status: AttendingStatus.AttendingVirtually,
                eventStartDate: DateTime.UtcNow.AddDays(10),
                maxVirtualParticipants: 4,
                maxParticipants: 0,
                participantCount: 4,
                addToQueueCount: 3);

            // Act
            await _sut.UpdateEventQueues(DefaultOrganizationName);

            // Assert
            Assert.That(@event.EventParticipants, Is.All.Matches<EventParticipant>(p => !p.IsInQueue));
        }

        [Test]
        public async Task Should_Keep_All_Participants_In_Virtual_Queue()
        {
            // Arrange
            var @event = MockEventForQueueUpdateTests(
                status: AttendingStatus.AttendingVirtually,
                eventStartDate: DateTime.UtcNow.AddDays(10),
                maxVirtualParticipants: 0,
                maxParticipants: 0,
                participantCount: 4,
                addToQueueCount: 4);

            // Act
            await _sut.UpdateEventQueues(DefaultOrganizationName);

            // Assert
            Assert.That(@event.EventParticipants, Is.All.Matches<EventParticipant>(p => p.IsInQueue));
        }

        [Test]
        public async Task Should_Remove_All_Participants_From_Normal_Queue()
        {
            // Arrange
            var @event = MockEventForQueueUpdateTests(
                status: AttendingStatus.Attending,
                eventStartDate: DateTime.UtcNow.AddDays(10),
                maxVirtualParticipants: 0,
                maxParticipants: 4,
                participantCount: 4,
                addToQueueCount: 4);

            // Act
            await _sut.UpdateEventQueues(DefaultOrganizationName);

            // Assert
            Assert.That(@event.EventParticipants, Is.All.Matches<EventParticipant>(p => !p.IsInQueue));
        }

        [Test]
        public async Task Should_Remove_One_Participant_From_Normal_Queue()
        {
            // Arrange
            var @event = MockEventForQueueUpdateTests(
                status: AttendingStatus.Attending,
                eventStartDate: DateTime.UtcNow.AddDays(10),
                maxVirtualParticipants: 0,
                maxParticipants: 4,
                participantCount: 5,
                addToQueueCount: 2);

            // Act
            await _sut.UpdateEventQueues(DefaultOrganizationName);

            // Assert
            Assert.AreEqual(1, @event.EventParticipants.Count(p => p.IsInQueue));
        }

        [Test]
        public async Task Should_Keep_All_Participants_In_Normal_Queue()
        {
            // Arrange
            var @event = MockEventForQueueUpdateTests(
                status: AttendingStatus.Attending,
                eventStartDate: DateTime.UtcNow.AddDays(10),
                maxVirtualParticipants: 0,
                maxParticipants: 0,
                participantCount: 4,
                addToQueueCount: 4);

            // Act
            await _sut.UpdateEventQueues(DefaultOrganizationName);

            // Assert
            Assert.That(@event.EventParticipants, Is.All.Matches<EventParticipant>(p => p.IsInQueue));
        }

        [Test]
        public async Task Should_Remove_All_Participants_From_Queue()
        {
            // Arrange
            var @event = MockEventForQueueUpdateTests(
                status: AttendingStatus.Attending,
                eventStartDate: DateTime.UtcNow.AddDays(-10),
                maxVirtualParticipants: 0,
                maxParticipants: 4,
                participantCount: 20,
                addToQueueCount: 16);

            // Act
            await _sut.UpdateEventQueues(DefaultOrganizationName);

            // Assert
            Assert.That(@event.EventParticipants, Is.All.Matches<EventParticipant>(p => !p.IsInQueue));
        }

        private Event MockEventForQueueUpdateTests(
            AttendingStatus status,
            DateTime eventStartDate,
            int maxVirtualParticipants,
            int maxParticipants,
            int participantCount,
            int addToQueueCount)
        {
            var eventId = Guid.NewGuid();
            var participants = new List<EventParticipant>();
            for (var i = 0; i < participantCount; i++)
            {
                participants.Add(new EventParticipant
                {
                    AttendStatus = (int)status,
                    IsInQueue = i < addToQueueCount
                });
            }

            var @event = new Event
            {
                Id = eventId,
                StartDate = eventStartDate,
                OrganizationId = DefaultOrganizationId,
                MaxVirtualParticipants = maxVirtualParticipants,
                MaxParticipants = maxParticipants,
                EventParticipants = participants
            };
            _eventsDbSet.SetDbSetDataForAsync(new List<Event> { @event });
            _organizationService
                .GetOrganizationByNameAsync(Arg.Is(DefaultOrganizationName))
                .Returns(new Organization
                {
                    Name = DefaultOrganizationName,
                    ShortName = DefaultOrganizationName,
                    Id = DefaultOrganizationId
                });
            return @event;
        }
    }
}
