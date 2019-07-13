﻿namespace MyResourcePlanning.Web.ViewModels.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyResourcePlanning.Web.Infrastructure.Validators;

    public class RequestCreateBindingModel
    {
        [Required]
        [DateGreaterOrEqualThatPresent]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan("StartDate")]
        [DateGreaterOrEqualThatPresent]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Projects")]
        [DataType(DataType.Text)]
        public string Project { get; set; }

        [Required]
        [Display(Name = "Resources")]
        [DataType(DataType.Text)]
        public string Resource { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage ="Please input positive hours")]
        [Display(Name = "Working Hours")]
        public double Hours { get; set; }
    }
}
