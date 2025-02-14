﻿using System;
using System.ComponentModel.DataAnnotations;

using MyResourcePlanning.Web.Infrastructure.Validators;

namespace MyResourcePlanning.Web.BindingModels.Project
{
    public class ProjectCreateBindingModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DateGreaterOrEqualThanPresent(ErrorMessage = "Date cannot be in the past")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Text)]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be greater than Start Date")]
        [DateGreaterOrEqualThanPresent(ErrorMessage = "Date cannot be in the past")]
        [Display(Name = "End Date")]
        [DataType(DataType.Text)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please Enter positive number")]
        [Display(Name = "Requested Hours")]
        public double RequestedHours { get; set; }
    }
}
