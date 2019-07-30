﻿namespace MyResourcePlanning.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Calendar;
    using MyResourcePlanning.Web.BindingModels.Calendar;
    using MyResourcePlanning.Web.ViewModels.Calendar;

    public class CalendarController : BaseController
    {
        private readonly ICalendarService calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CalnedarCreatePeriodBindingModel inputModel)
        {
            if (await this.calendarService.CheckIfPeriodExist(inputModel))
            {
                this.ModelState.AddModelError("ErrorMessage", "Some days in the period are already added!");
                return this.View(inputModel ?? new CalnedarCreatePeriodBindingModel());
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new CalnedarCreatePeriodBindingModel());
            }

            await this.calendarService.CreatePeriod(inputModel);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var requests = await this.calendarService.GetAllDays<CalendarAllViewModel>();
            return this.View(requests);
        }
    }
}
