using System;
using System.ComponentModel.DataAnnotations;

using MyResourcePlanning.Web.Infrastructure.Validators;

namespace MyResourcePlanning.Web.BindingModels.Request
{
    public class RequestCreateBindingModel
    {
        [Required]
        [DateGreaterOrEqualThatPresent(ErrorMessage = "Date cannot be in the past")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Text)]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be greater than Start Date")]
        [DateGreaterOrEqualThatPresent(ErrorMessage = "Date cannot be in the past")]
        [Display(Name = "End Date")]
        [DataType(DataType.Text)]
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
        [Range(1, double.MaxValue, ErrorMessage = "Please input positive hours")]
        [Display(Name = "Working Hours")]
        public double WorkingHours { get; set; }
    }
}
