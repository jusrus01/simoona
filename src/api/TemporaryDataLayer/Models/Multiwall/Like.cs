﻿using System;
using Shrooms.Contracts.Enums;

namespace TemporaryDataLayer
{
    public class Like
    {
        public string UserId { get; private set; }

        public DateTime Created { get; private set; }

        public LikeTypeEnum Type { get; private set; }

        public Like(string userId, LikeTypeEnum type)
        {
            UserId = userId;
            Created = DateTime.UtcNow;
            Type = type;
        }
    }
}