﻿using System;
using System.Collections.Generic;

namespace Shrooms.DataLayer.EntityModels.Models.Committee
{
    public class CommitteeSuggestion : BaseModel
    {
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
