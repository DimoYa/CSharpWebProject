namespace MyResourcePlanning.Web.Controllers
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

            return this.RedirectToAction(nameof(this.All));
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

            await this.calendarService.Edit(model, id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.calendarService.Delete(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var requests = await this.calendarService.GetAllDays<CalendarAllViewModel>();
            return this.View(requests);
        }

        public async Task<IActionResult> CreateAbsence()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbsence(CalendarCreateAbsenceBindingModel inputModel)
        {
            if (await this.calendarService.CheckIfPeriodExist(inputModel.StartDate, inputModel.EndDate) == false)
            {
                this.ModelState.AddModelError("ErrorMessage", "Some days in the period are not present in the calendar!");
                return this.View(inputModel ?? new CalendarCreateAbsenceBindingModel());
            }

            if (await this.calendarService.CheckIfAbsenceExistOrIsPublicHoliday(inputModel.StartDate, inputModel.EndDate))
            {
                this.ModelState.AddModelError("ErrorMessage", "Some days in the period are already added as absence or marked as public holiday!");
                return this.View(inputModel ?? new CalendarCreateAbsenceBindingModel());
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new CalendarCreateAbsenceBindingModel());
            }

            await this.calendarService.CreateAbsence(inputModel);

            return this.RedirectToAction(nameof(this.MyCalendar));
        }

        public async Task<IActionResult> DeleteAbsence(string id)
        {
            await this.calendarService.DeleteAbsence(id);

            return this.RedirectToAction(nameof(this.MyCalendar));
        }

        public async Task<IActionResult> MyCalendar()
        {
            var requests = await this.calendarService.GetMyDays<CalendarMyViewModel>();
            return this.View(requests);
        }
    }
}
