using System;

namespace Shrooms.DataLayer.EntityModels.ModelsCore
{
    public class WorkingHours : BaseModelWithOrg
    {
        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public TimeSpan? LunchStart { get; set; }

        public TimeSpan? LunchEnd { get; set; }

        public bool FullTime { get; set; }

        public int? PartTimeHours { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }
    }
}