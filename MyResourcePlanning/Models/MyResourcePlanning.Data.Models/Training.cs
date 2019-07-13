namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Data.Models.BaseModels;
    using MyResourcePlanning.Data.Models.Enums;

    public class Training : BaseDeletableModel<string>
    {
        public Training()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserTraining>();
        }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public TrainingType Type { get; set; }

        public virtual ICollection<UserTraining> Users { get; set; }
    }
}
