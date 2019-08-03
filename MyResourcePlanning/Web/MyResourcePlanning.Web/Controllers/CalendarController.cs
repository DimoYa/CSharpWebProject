namespace MyResourcePlanning.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Common;
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," +
                           GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> All()
        {
            var requests = await this.calendarService.GetAllDays<CalendarAllViewModel>();
            return this.View(requests);
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> CreateAbsence()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
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

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> DeleteAbsence(string id)
        {
            await this.calendarService.DeleteAbsence(id);

            return this.RedirectToAction(nameof(this.MyCalendar));
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> MyCalendar()
        {
            var requests = await this.calendarService.GetMyDays<CalendarMyViewModel>();
            return this.View(requests);
        }
    }
}
