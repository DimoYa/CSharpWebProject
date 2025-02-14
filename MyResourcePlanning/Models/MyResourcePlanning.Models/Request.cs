﻿namespace MyResourcePlanning.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Models.BaseModels;
    using MyResourcePlanning.Models.Enums;
    using MyResourcePlanning.Web.Infrastructure.Validators;

    public class Request : BaseDeletableModel<string>
    {
        public Request()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.Status = RequestStatus.InProgress;
        }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        [DateGreaterOrEqualThanPresent]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan(nameof(StartDate))]
        [DateGreaterOrEqualThanPresent]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double WorkingHours { get; set; }

        [Required]
        public RequestStatus Status { get; set; }

        [MinLength(1)]
        public string Comment { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
