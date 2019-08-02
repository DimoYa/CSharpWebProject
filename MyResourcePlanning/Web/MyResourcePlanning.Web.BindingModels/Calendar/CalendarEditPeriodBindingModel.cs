namespace MyResourcePlanning.Web.BindingModels.Calendar
{
    using MyResourcePlanning.Services.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;
    using Calendar = MyResourcePlanning.Models.Calendar;
    public class CalendarEditPeriodBindingModel : IMapFrom<Calendar>
    {
        [Display(Name = "Date")]
        public DateTime Day { get; set; }

        [Required]
        [Display(Name = "Public holiday")]
        public bool IspublicHoliday { get; set; }
      
    }
}
