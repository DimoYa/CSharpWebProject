namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Data.Models.Enums;

    public class Training
    {
        public Training()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserTrainings>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public TrainingType Type { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<UserTrainings> Users { get; set; }
    }
}
