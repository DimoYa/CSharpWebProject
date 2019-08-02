namespace MyResourcePlanning.Web.BindingModels.Calendar
{
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.Infrastructure.Validators;
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using System.Globalization;
    using Calendar = Models.Calendar;
    using MyResourcePlanning.Web.ViewModels.Calendar;

    public class CalnedarEditPeriodBindingModel : IMapFrom<Calendar>
    {
        [Display(Name = "Date")]
        public DateTime Day { get; set; }

        [Required]
        [Display(Name = "Public holiday")]
        public bool IspublicHoliday { get; set; }
      
    }
}
