﻿using System.Collections.Generic;

namespace TemporaryDataLayer
{
    public class BadgeCategory : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BadgeType> BadgeTypes { get; set; }
        public virtual ICollection<BadgeCategoryKudosType> RelationshipsWithKudosTypes { get; set; }
    }
}
