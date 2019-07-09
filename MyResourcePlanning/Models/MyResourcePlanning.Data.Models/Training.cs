namespace MyResourcePlanning.Data.Models
{
    using MyResourcePlanning.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Training
    {
        public Training()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserTraining>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public TrainingType Type { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<UserTraining> Users { get; set; }
    }
}
