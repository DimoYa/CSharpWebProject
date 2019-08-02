using MyResourcePlanning.Models.Enums;
using MyResourcePlanning.Web.Infrastructure.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyResourcePlanning.Web.BindingModels.Calendar
{
    public class CalendarEditAbsenceBindingModel
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
        [EnumDataType(typeof(SkillLevel))]
        [Display(Name = "Absence Type")]
        public UserCalendarAbsenceType AbsenceType { get; set; }

        public string ErrorMessage { get; set; }

    }
}
