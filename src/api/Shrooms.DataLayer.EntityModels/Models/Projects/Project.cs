using Shrooms.DataLayer.EntityModels.Models.Multiwalls;
using System.Collections.Generic;
using System.Linq;

namespace Shrooms.DataLayer.EntityModels.Models.Projects
{
    public class Project : BaseModelWithOrg
    {
        public string Name { get; set; }

        public string Desc { get; set; }

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual IEnumerable<ApplicationUser> Members 
        {
            get => ProjectApplicationUsers.Select(model => model.ApplicationUser);
        }

        public virtual IEnumerable<Skill> Attributes 
        {
            get => ProjectSkills.Select(model => model.Skill);
        }

        public int WallId { get; set; }

        public virtual Wall Wall { get; set; }

        public string Logo { get; set; }

        // Required for many-to-many
        public ICollection<ProjectSkill> ProjectSkills { get; set; }

        public ICollection<ProjectApplicationUser> ProjectApplicationUsers { get; set; }
    }
}
