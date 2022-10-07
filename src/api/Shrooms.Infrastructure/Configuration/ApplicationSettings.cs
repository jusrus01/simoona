﻿using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using Microsoft.Extensions.Configuration;
using Shrooms.Contracts.Infrastructure;

namespace Shrooms.Infrastructure.Configuration
{
    public class ApplicationSettings : IApplicationSettings
    {
        private readonly IConfiguration _configuration;

        public ApplicationSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string StorageConnectionString => _configuration.GetConnectionString("StorageConnectionString");

        public bool IsEmailEnabled => bool.Parse(ConfigurationManager.AppSettings["EmailEnabled"]);

        public int DefaultOrganizationId => int.Parse(ConfigurationManager.AppSettings["DefaultOrganizationId"]);

        public int AccessTokenLifeTimeInHours => int.Parse(ConfigurationManager.AppSettings["AccessTokenLifeTimeInHours"]);

        public bool IsProductionBuild => bool.TryParse(ConfigurationManager.AppSettings["IsProductionBuild"], out var result) && result;

        public IEnumerable<string> OAuthRedirectUris => ConfigurationManager.AppSettings["OAuthRedirectUri"].Split(',');

        public string DemoAccountDefaultPictureId => ConfigurationManager.AppSettings["DemoAccountDefaultPictureID"];

        public string ClientUrl => string.Format(ConfigurationManager.AppSettings["ClientUrl"]);

        public string BasicUsername => _configuration.GetSection("BasicUsername").Get<string>();

        public string BasicPassword => _configuration.GetSection("BasicPassword").Get<string>();

        public string CorsOriginsSetting => ConfigurationManager.AppSettings["CorsOriginsSettingKey"];

        public string SupportEmail => ConfigurationManager.AppSettings["SupportEmail"];

        public string VacationsBotAuthToken => ConfigurationManager.AppSettings["VacationsBotAuthToken"];

        public string VacationsBotHistoryUrl => ConfigurationManager.AppSettings["VacationsBotHistoryUrl"];

        public string ClientUrlWithOrg(string tenant) => GetClientPath(tenant);

        public string PictureUrl(string tenantPicturesContainer, string pictureName) => GetClientPath($"api/storage/{tenantPicturesContainer.ToLowerInvariant()}/{pictureName}");

        public string WallPostUrl(string organization, int postId) => GetClientPath($"{organization}/Wall/feed?post={postId}");

        public string UserNotificationSettingsUrl(string tenant) => GetClientPath($"{tenant}/Settings/Notifications");

        public string UserProfileUrl(string tenant, string userId) => GetClientPath($"{tenant}/profiles/{userId}");

        public string BookUrl(string tenant, int bookOfficeId, int officeId) => GetClientPath($"{tenant}/Books/Edit/{bookOfficeId}/{officeId}");

        public string KudosProfileUrl(string tenant, string userId) => GetClientPath($"{tenant}/Kudos/KudosUserInformation/{userId}");

        public string EventUrl(string tenant, string eventId) => GetClientPath($"{tenant}/Events/EventContent/{eventId}");

        public string EventListByTypeUrl(string tenant, string eventTypeId) => GetClientPath($"{tenant}/Events/List/{eventTypeId}/office/all");

        public string ProjectUrl(string tenant, string projectId) => GetClientPath($"{tenant}/Projects/Details/{projectId}");

        public string CommitteeSugestionUrl(string tenant) => GetClientPath($"{tenant}/Committees/List");

        public string ServiceRequestUrl(string tenant, int id) => GetClientPath($"{tenant}/ServiceRequests/List?Id={id}");

        public string ResetPasswordUrl(string organization, string userName, string token) => GetClientPath($"{organization}/Reset?UserName={HttpUtility.UrlEncode(userName)}&Token={HttpUtility.UrlEncode(token)}");

        public string VerifyEmailUrl(string organization, string userName, string token) => GetClientPath($"{organization}/Verify?UserName={HttpUtility.UrlEncode(userName)}&Token={HttpUtility.UrlEncode(token)}");

        public string FeedUrl(string tenant) => GetClientPath($"{tenant}/Wall/Feed");

        public string ApiUrl => string.Format(ConfigurationManager.AppSettings["ApiUrl"]);

        private string GetClientPath(string relativePath) => Path.Combine(ClientUrl, relativePath);
    }
}