namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Skill
    {
        public Skill()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserSkills>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<UserSkills> Users { get; set; }
    }
}
