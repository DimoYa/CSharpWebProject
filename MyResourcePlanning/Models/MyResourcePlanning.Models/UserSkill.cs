namespace MyResourcePlanning.Models
{
    public class UserSkill
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string SkillId { get; set; }

        public Skill Skill { get; set; }

        public bool IsAllowedToAdd { get; set; }
    }
}
