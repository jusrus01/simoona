using Shrooms.DataLayer.EntityModels.Models.Projects;
using Shrooms.DataLayer.EntityModels.Models.Users;
using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.Models
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
        public ICollection<ProjectSkill> ProjectSkills { get; set; }

        public ICollection<ApplicationUserSkill> ApplicationUserSkills { get; set; }
    }
}
