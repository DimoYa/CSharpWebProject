namespace MyResourcePlanning.Services.Data.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Web.BindingModels.Calendar;

    public interface ICalendarService
    {
        Task<bool> CreatePeriod(CalendarCreatePeriodBindingModel inputModel);

        Task<bool> CheckIfPeriodExist(DateTime startDate, DateTime endDate);

        Task<bool> CheckIfAbsenceExistOrIsPublicHoliday(DateTime startDate, DateTime endDate);

        Task<bool> EditPeriod(CalendarEditPeriodBindingModel model, string id);

        Task<bool> DeletePeriod(string id);

        Task<TViewModel> MapPeriod<TViewModel>(string id);

        Task<bool> CreateAbsence(CalendarCreateAbsenceBindingModel inputModel);

        Task<bool> DeleteAbsence(string calendarId);

        Task<IEnumerable<TViewModel>> GetAllDays<TViewModel>();

        Task<IEnumerable<TViewModel>> GetMyAbsenceDays<TViewModel>();
    }
}
