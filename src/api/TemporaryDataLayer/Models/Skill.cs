using System.Collections.Generic;
using System.Linq;
using TemporaryDataLayer.Models.Users;

namespace TemporaryDataLayer
{
    public class Skill : BaseModel
    {
        public string Title { get; set; }

        public bool ShowInAutoComplete { get; set; }

        public virtual IEnumerable<ApplicationUser> ApplicationUsers 
        { 
            get => ApplicationUserSkills.Select(model => model.ApplicationUser); 
        }

        // Required for many-to-many
        internal ICollection<ApplicationUserSkill> ApplicationUserSkills { get; set; }

        //public virtual ICollection<Project> Projects { get; set; }
    }
}
