﻿using System.Collections.Generic;
using System.Linq;
using Shrooms.Contracts.Enums;

namespace Shrooms.DataLayer.EntityModels.ModelsCore.Notifications
{
    public class Notification : BaseModelWithOrg
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string PictureId { get; set; }

        public NotificationType Type { get; set; }

        public virtual ICollection<NotificationUser> NotificationUsers { get; set; }

        public Sources Sources { get; set; }

        public static Notification Create(string title, string description, string pictureId, Sources sourceIds, NotificationType type, int organizationId, IEnumerable<string> membersToNotify)
        {
            var notification = new Notification
            {
                Description = description,
                PictureId = pictureId,
                Title = title,
                Type = type,
                Sources = sourceIds,
                OrganizationId = organizationId
            };

            notification.NotificationUsers = membersToNotify
                .Select(m => new NotificationUser
                {
                    IsAlreadySeen = false,
                    UserId = m
                })
                .ToList();

            return notification;
        }
    }
}