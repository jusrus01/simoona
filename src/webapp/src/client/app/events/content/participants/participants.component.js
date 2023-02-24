(function () {
    'use strict';

    angular.module('simoonaApp.Events').component('aceEventParticipants', {
        bindings: {
            event: '=',
            isAdmin: '=',
            isLoading: '=',
        },
        templateUrl: 'app/events/content/participants/participants.html',
        controller: eventParticipantsController,
        controllerAs: 'vm',
    });

    eventParticipantsController.$inject = [
        'eventRepository',
        'authService',
        'eventParticipantsService',
        'eventStatusService',
        'eventStatus',
        'eventService',
        'errorHandler',
        'lodash',
        'Analytics',
        'attendStatus'
    ];

    function eventParticipantsController(
        eventRepository,
        authService,
        eventParticipantsService,
        eventStatusService,
        eventStatus,
        eventService,
        errorHandler,
        lodash,
        Analytics,
        attendStatus
    ) {
        /* jshint validthis: true */
        var vm = this;

        vm.isParticipantsLoading = false;
        vm.isMainParticipantList = true;

        vm.eventStatus = eventStatus;
        vm.eventStatusService = eventStatusService;
        vm.participantsTabs = [
            {
                name: 'ParticipantsList',
                isOpen: true,
            },
            {
                name: 'OptionsList',
                isOpen: false,
            },
        ];

        vm.goToTab = goToTab;
        vm.expelUserFromEvent = expelUserFromEvent;
        vm.isDeleteVisible = isDeleteVisible;
        vm.isActiveTab = isActiveTab;
        vm.isExportVisible = isExportVisible;
        vm.getTotalGoingParticipantCount = getTotalGoingParticipantCount;
        vm.getTotalMaxParticipantCount = getTotalMaxParticipantCount;

        /////////

        function goToTab(tab) {
            vm.participantsTabs.forEach(function (item) {
                if (tab === item.name) {
                    item.isOpen = true;
                } else {
                    item.isOpen = false;
                }
            });
        }

        function isActiveTab(tab) {
            return !!lodash.find(vm.participantsTabs, function (obj) {
                return !!obj.isOpen && obj.name === tab;
            });
        }

        function isDeleteVisible() {
            return (
                vm.isAdmin &&
                eventStatusService.getEventStatus(vm.event) !==
                    eventStatus.Finished
            );
        }

        function isExportVisible() {
            return getTotalGoingParticipantCount() > 0;
        }

        function getTotalGoingParticipantCount() {
            return eventService.getTotalGoingParticipantCount(vm.event);
        }

        function getTotalMaxParticipantCount() {
            return eventService.getTotalMaxParticipantCount(vm.event);
        }

        function expelUserFromEvent(participant) {
            Analytics.trackEvent(
                'Events',
                'expelUserFromEvent: ' + participant.userId,
                'Event: ' + vm.event.id
            );
            if (!participant.isLoading) {
                participant.isLoading = true;

                return eventRepository
                    .expelUserFromEvent(vm.event.id, participant.userId)
                    .then(
                        function (response) {
                            moveParticipantFromQueueToParticipant(response);
                            removeParticipant(response);
                            recalculateParticipants();
                            participant.isLoading = false;
                            return response;
                        },
                        function (response) {
                            participant.isLoading = false;
                            errorHandler.handleErrorMessage(
                                response,
                                'expelParticipant'
                            );
                        }
                    );
            }
        }

        function recalculateParticipants() {
            vm.event.participantsCount =
                eventService.countAttendingParticipants(
                    vm.event
                );
            vm.event.virtualParticipantsCount =
                eventService.countVirtuallyAttendingParticipants(
                    vm.event
                );
            vm.event.goingCount = vm.event.participantsCount;
            vm.event.virtuallyGoingCount = vm.event.virtualParticipantsCount;
        }

        function moveParticipantFromQueueToParticipant(response) {
            var nextParticipant = response.nextParticipant;
            if (!nextParticipant) {
                return;
            }

            eventParticipantsService.toggleQueueStatus(
                vm.event.participants,
                nextParticipant.userId
            );

            if (isCurrentUser(nextParticipant)) {
                vm.event.isInQueue = false;
            }
        }

        function removeParticipant(response) {
            var removedParticipant = response.removedParticipant;
            eventParticipantsService.removeParticipant(
                vm.event.participants,
                removedParticipant.userId
            );
            eventParticipantsService.removeParticipantFromOptions(
                vm.event.options,
                removedParticipant.userId
            );

            if (isCurrentUser(removedParticipant)) {
                vm.event.participatingStatus = attendStatus.Idle;
            }
        }

        function isCurrentUser(participant) {
            return authService.identity.userId ===
                participant.userId;
        }
    }
})();
