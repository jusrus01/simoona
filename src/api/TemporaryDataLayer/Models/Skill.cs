using System.Collections.Generic;
using System.Linq;
using TemporaryDataLayer.Models;
using TemporaryDataLayer.Models.Users;

namespace TemporaryDataLayer
{
    public class Skill : BaseModel
    {
        public string Title { get; set; }

        public bool ShowInAutoComplete { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers 
        { 
            get => ApplicationUserSkills.Select(model => model.ApplicationUser); 
        }

        public IEnumerable<Project> Projects 
        { 
            get => ProjectSkills.Select(model => model.Project); 
        }

        // Required for many-to-many
        internal ICollection<ProjectSkill> ProjectSkills { get; set; }

        internal ICollection<ApplicationUserSkill> ApplicationUserSkills { get; set; }
    }
}
