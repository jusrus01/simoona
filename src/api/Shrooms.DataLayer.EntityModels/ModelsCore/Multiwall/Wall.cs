﻿using System.Collections.Generic;
using Shrooms.Contracts.Enums;

namespace Shrooms.DataLayer.EntityModels.ModelsCore.Multiwalls
{
    public class Wall : BaseModelWithOrg
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Logo { get; set; }

        public bool IsHiddenFromAllWalls { get; set; }

        public bool AddForNewUsers { get; set; }

        public WallType Type { get; set; }

        public WallAccess Access { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<WallMember> Members { get; set; }

        public virtual ICollection<WallModerator> Moderators { get; set; }
    }
}
