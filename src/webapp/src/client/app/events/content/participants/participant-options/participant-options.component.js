(function() {
    'use strict';

    angular
        .module('simoonaApp.Events')
        .component('aceEventParticipantOptions', {
            bindings: {
                options: '=',
                participants: '='
            },
            templateUrl: 'app/events/content/participants/participant-options/participant-options.html',
            controller: eventParticipantOptionsController,
            controllerAs: 'vm'
        });

    eventParticipantOptionsController.$inject = [
        '$scope',
        'authService',
        'lodash',
        'attendStatus'
    ];

    function eventParticipantOptionsController($scope, authService, lodash, attendStatus) {
        /* jshint validthis: true */
        var vm = this;

        vm.accordionOptionArray = [];
        vm.hasCurrentUserSelectedOption = hasCurrentUserSelectedOption;
        vm.attendingParticipants = getAttendingParticipants;

        $scope.$watchCollection('options', function() {
            vm.joinedParticipantsOptions = removeInQueueParticipants();
        });

        ////////

        function removeInQueueParticipants() {
            return vm.options.map(option => Object.assign(option, {
                participants: option.participants.filter(participant => !participant.isInQueue)
            }));
        }

        function getAttendingParticipants() {
            return getParticipantsByStatuses([attendStatus.Attending, attendStatus.AttendingVirtually]);
        }

        function hasCurrentUserSelectedOption(participants) {
            var currentUserId = authService.identity.userId;

            return !!lodash.find(participants, function(obj) {
                return obj.userId === currentUserId;
            });
        }

        function getParticipantsByStatuses(statuses) {
            return vm.participants.filter(participant =>
                statuses.includes(participant.attendStatus) &&
                !participant.isInQueue);
        }
    }
})();
