namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Common.Validators;

    public class Project
    {
        public Project()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Requests = new HashSet<Request>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DateGreaterOrEqualThatPresent]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan("StartDate")]
        [DateGreaterOrEqualThatPresent]
        public DateTime EndDate { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
