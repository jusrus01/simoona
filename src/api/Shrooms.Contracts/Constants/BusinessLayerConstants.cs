﻿using System.Collections.Generic;
using System.Globalization;

namespace Shrooms.Contracts.Constants
{
    public static class BusinessLayerConstants
    {
        public static readonly IEnumerable<CultureInfo> SupportedLanguages = new[] { CultureInfo.GetCultureInfo("en-US"), CultureInfo.GetCultureInfo("lt-LT") };

        public const string DefaultCulture = "en-US";

        public const string DefaultEmailLinkName = "Simoona";
        public const string DefaultSignatureTemplate = "<hr><u></u><div>{0} <a href='{1}' target='_blank'>{2}</a>.</div>";
        public const string DefaultEmailTemplate = "<div>{0}</div>{1}";
        public const int MaxMessageLength = 300;
        public const int WallKudosLogCount = 5;
        public static readonly char[] SearchSplitter = { ' ', ',', ';', '|' };

        public const string DeletedUserName = "Deleted Account";
        public const string DeletedUserFirstName = "Deleted";
        public const string DeletedUserLastName = "Account";

        #region Walls

        #endregion

        #region Kudos
        public const string KudosServiceCategory = "Kudos";
        public const string KudosServiceStatusDone = "Done";
        public const string KudosServiceStatusCancelled = "Cancelled";
        public const string KudosStatusAllFilter = "All";
        public const string KudosFilteringTypeAllFilter = "All";
        public const int KudosAvailableToSendThisMonth = 20;
        public const int MaxKudosLogsPerPage = 50;

        public const int MaxKudosDescriptionLength = 500;

        public const string KudosLogExcelSheetName = "Kudos logs";

        #endregion

        #region ShroomsInfo
        public const string ShroomsInfoEmailSubject = "Simoona wall";
        public const string WallPostsListTemplate = "<tr><td><b><u>({0}) {1}</u></b>:<br/>{2}</td></tr>";
        public const string ShroomsInfoEmailMessageBodyTemplate = "<table cellspacing='0' cellpadding='5' border='1'>{0}</table><br/>For more news check our <a href='{1}/{2}' target='_blank'>Simoona</a>";
        #endregion

        public const int LogsPerPage = 100;
        public const int MinCharactersInLeadearboardSearch = 2;
        public const string SimonaUrl = "http://simona:8888";

        #region MailingService
        public const string FromEmailAddress = "noreply@simoona.com";
        public const string EmailSenderName = "Simoona";
        #endregion

        #region Books
        public const int BooksPerPage = 10;
        public const int MinCharactersInBookSearch = 2;
        public const string LoyaltyBotName = "Loyalty bot";

        #endregion

        #region Lotteries

        public const string LotteryParticipantsExcelTableName = "Lottery Participants";
        public const int LotteryParticipantsInRow = 3;
        #endregion

        #region Organization
        public const int MaxOrganizationNameLength = 300;
        public const int MinOrganizationNameLength = 2;
        public const int MaxOrganizationShortNameLength = 64;
        public const int WelcomeEmailLength = 10000;
        public const int MaxHostNameLength = 50;
        #endregion

        public const int MaxNotificationsToShow = 100;
    }
}