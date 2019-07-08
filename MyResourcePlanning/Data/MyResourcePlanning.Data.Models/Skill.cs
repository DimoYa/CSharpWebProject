namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Skill
    {
        public Skill()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserSkills>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<UserSkills> Users { get; set; }
    }
}
