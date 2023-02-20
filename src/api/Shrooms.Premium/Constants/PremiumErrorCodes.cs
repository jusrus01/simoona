﻿namespace Shrooms.Premium.Constants
{
    public static class PremiumErrorCodes
    {
        // KudosShop
        public const int KudosShopItemAlreadyExist = 350;

        // Events, 2**
        public const string EventCreateStartDateIncorrectCode = "200";
        public const string EventStartDateGreaterThanEndDateCode = "201";
        public const string EventResponsiblePersonDoesNotExistCode = "202";
        public const string EventInsufficientOptionsCode = "203";
        public const string EventTypeDoesNotExistCode = "204";
        public const string EventNeedToHaveMaxChoiceCode = "205";
        public const string EventDoesNotExistCode = "206";
        public const string EventDontHavePermissionCode = "207";
        public const string EventNotEnoughChoicesProvidedCode = "208";
        public const string EventHasAlreadyExpiredCode = "209";
        public const string EventTooManyChoicesProvidedCode = "210";
        public const string EventIsFullCode = "211";
        public const string EventUserAlreadyParticipatesCode = "212";
        public const string EventJoinStartDateHasPassedCode = "213";
        public const string EventCannotJoinMultipleSingleJoinEventsCode = "214";
        public const string EventNoSuchOptionsCode = "215";
        public const string EventRegistrationDeadlineGreaterThanStartDateCode = "216";
        public const string EventRegistrationDeadlineIsExpired = "217";
        public const string EventJoinUserDoesNotExists = "218";
        public const string EventOptionsCantDuplicate = "219";
        public const string EventParticipantNotFound = "220";
        public const string EventParticipantsNotFound = "221";
        public const int EventTypeNameAlreadyExists = 222;
        public const string EventChoiceCanBeSingleOnly = "223";
        public const string EventWrongAttendStatus = "224";
        public const string EventUserNotParticipating = "225";
        public const string EventDateFilterRangeInvalid = "226";
        public const string EventAttendTypeIsNotAllowed = "227";
        public const string EventAttendTypeCannotBeChangedIfParticipantsJoined = "228";
        public const string EventInvalidReminderType = "229";
        public const string EventReminderCannotBeRemoved = "230";
        public const string EventReminderCannotBeUpdated = "231";
        public const string EventReminderCannotBeAdded = "232";
        public const string EventParticipantCannotBeAdded = "233";

        // Service Request, 7**
        public const int ServiceRequestIsClosed = 700;

        // Vacation, 10**
        public const int VacationBotError = 1000;
    }
}
