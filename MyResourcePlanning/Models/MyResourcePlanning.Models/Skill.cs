namespace MyResourcePlanning.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Models.BaseModels;

    public class Skill : BaseDeletableModel<string>
    {
        public Skill()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserSkill>();
        }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        public string SkillCategoryId { get; set; }

        public SkillCategory SkillCategory { get; set; }

        public virtual ICollection<UserSkill> Users { get; set; }
    }
}
