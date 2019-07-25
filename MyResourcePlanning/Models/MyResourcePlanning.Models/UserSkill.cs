using MyResourcePlanning.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyResourcePlanning.Models
{
    public class UserSkill
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string SkillId { get; set; }

        public bool IsDeleted { get; set; }

        public Skill Skill { get; set; }

        [Required]
        public SkillLevel Level { get; set; }

    }
}
