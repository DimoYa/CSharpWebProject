namespace MyResourcePlanning.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Models.BaseModels;
    using MyResourcePlanning.Models.Enums;

    public class Skill : BaseDeletableModel<string>
    {
        public Skill()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserSkill>();
        }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        public string SkillCategoryId { get; set; }

        [Required]
        public SkillLevel Level { get; set; }

        public SkillCategory SkillCategory { get; set; }

        public virtual ICollection<UserSkill> Users { get; set; }
    }
}
