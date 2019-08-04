namespace MyResourcePlanning.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Models.BaseModels;
    using MyResourcePlanning.Models.Enums;
    using MyResourcePlanning.Web.Infrastructure.Validators;

    public class Training : BaseDeletableModel<string>
    {
        public Training()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserTraining>();
        }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public TrainingType Type { get; set; }

        [Required]
        public TrainingStatus Status { get; set; }

        [Required]
        [DateGreaterOrEqualThatPresent]
        public DateTime DueDate { get; set; }

        public virtual ICollection<UserTraining> Users { get; set; }
    }
}
