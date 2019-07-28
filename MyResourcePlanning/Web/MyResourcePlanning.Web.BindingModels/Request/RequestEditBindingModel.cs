namespace MyResourcePlanning.Web.BindingModels.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyResourcePlanning.Web.Infrastructure.Validators;

    public class RequestEditBindingModel
    {
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Text)]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be greater than Start Date")]
        [Display(Name = "End Date")]
        [DataType(DataType.Text)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Projects")]
        [DataType(DataType.Text)]
        public string Project { get; set; }

        [Display(Name = "Resources")]
        [DataType(DataType.Text)]
        public string Resource { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please input positive hours")]
        [Display(Name = "Working Hours")]
        public double WorkingHours { get; set; }
    }
}
