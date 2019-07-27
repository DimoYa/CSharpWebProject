namespace MyResourcePlanning.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Models.BaseModels;
    using MyResourcePlanning.Web.Infrastructure.Validators;

    public class Project : BaseDeletableModel<string>
    {
        public Project()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Requests = new HashSet<Request>();
        }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [DateGreaterOrEqualThatPresent]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan("StartDate")]
        [DateGreaterOrEqualThatPresent]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double RequestedHours { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
