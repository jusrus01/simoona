﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TemporaryDataLayer
{
    public class WallModerator : BaseModel
    {
        [ForeignKey("Wall")]
        public int WallId { get; set; }

        public Wall Wall { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
