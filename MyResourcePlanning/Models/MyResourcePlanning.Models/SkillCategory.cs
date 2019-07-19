namespace MyResourcePlanning.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Models.BaseModels;

    public class SkillCategory : BaseDeletableModel<string>
    {
        public SkillCategory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.Skills = new HashSet<Skill>();
        }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
