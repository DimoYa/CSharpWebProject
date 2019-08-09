namespace MyResourcePlanning.Models
{
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Models.Enums;

    public class UserSkill
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string SkillId { get; set; }

        public Skill Skill { get; set; }

        [Required]
        public SkillLevel Level { get; set; }
    }
}
