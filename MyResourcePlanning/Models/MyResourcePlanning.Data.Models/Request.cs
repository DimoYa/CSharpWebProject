namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Data.Models.Enums;
    using MyResourcePlanning.Web.Infrastructure.Validators;

    public class Request
    {
        public Request()
        {
            this.CreatedAt = DateTime.Now;
            this.CreatedBy = this.User.Id;
        }

        [Required]
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        [Required]
        [DateGreaterOrEqualThatPresent]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan("StartDate")]
        [DateGreaterOrEqualThatPresent]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double WorkingHours { get; set; }

        [Required]
        public RequestStatus Status { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
