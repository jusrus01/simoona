(function() {
    'use strict';

    angular
        .module('simoonaApp.Events')
        .service('eventParticipantsService', eventParticipantsService);

    eventParticipantsService.$inject = [
        'lodash'
    ];

    function eventParticipantsService(lodash) {
        var service = {
            removeParticipant: removeParticipant,
            removeParticipantFromOptions: removeParticipantFromOptions,
            toggleQueueStatus: toggleQueueStatus
        };
        return service;

        /////////

        function toggleQueueStatus(participantList, userId) {
            for (var participant of participantList) {
                if (participant.userId == userId) {
                    participant.isInQueue = !participant.isInQueue;
                    return;
                }
            }
        }

        function removeParticipant(participantList, userId) {
            lodash.remove(participantList, function(participant) {
                return participant.userId === userId;
            });
        }

        function removeParticipantFromOptions(options, userId) {
            for (var i = 0; options.length > i; i++) {
                removeParticipant(options[i].participants, userId);
            }
        }
    }
})();
