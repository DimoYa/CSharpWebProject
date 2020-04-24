namespace MyResourcePlanning.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Calendar;
    using MyResourcePlanning.Web.BindingModels.Calendar;
    using MyResourcePlanning.Web.ViewModels.Calendar;

    public class CalendarController : AdminController
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
        public async Task<IActionResult> Create(CalendarCreatePeriodBindingModel inputModel)
        {
            if (await this.calendarService.CheckIfPeriodExist(inputModel.StartDate, inputModel.EndDate))
            {
                this.ModelState.AddModelError("ErrorMessage", "Some days in the period are already added!");
                return this.View(inputModel ?? new CalendarCreatePeriodBindingModel());
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new CalendarCreatePeriodBindingModel());
            }

            await this.calendarService.CreatePeriod(inputModel);

            return this.Redirect("/Calendar/All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var requestForUpdate = await this.calendarService.MapPeriod<CalendarAllViewModel>(id);

            return this.View(requestForUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CalendarEditPeriodBindingModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(new CalendarAllViewModel());
            }

            await this.calendarService.EditPeriod(model, id);

            return this.Redirect("/Calendar/All");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.calendarService.DeletePeriod(id);

            return this.Redirect("/Calendar/All");
        }
    }
}
