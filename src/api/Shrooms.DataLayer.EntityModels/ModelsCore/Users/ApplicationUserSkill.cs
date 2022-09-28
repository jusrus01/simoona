namespace Shrooms.DataLayer.EntityModels.ModelsCore.Users
{
    public class ApplicationUserSkill
    {
        public int SkillId { get; set; }

        public Skill Skill { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
