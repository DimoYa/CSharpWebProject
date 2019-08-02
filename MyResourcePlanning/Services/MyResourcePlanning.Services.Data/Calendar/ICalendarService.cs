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

        Task<bool> Edit(CalendarEditPeriodBindingModel model, string id);

        Task<bool> Delete(string id);

        Task<bool> CreateAbsence(CalendarCreateAbsenceBindingModel inputModel);

        Task<bool> DeleteAbsence(string id);

        Task<TViewModel> MapPeriod<TViewModel>(string id);

        Task<IEnumerable<TViewModel>> GetAllDays<TViewModel>();

        Task<IEnumerable<TViewModel>> GetMyDays<TViewModel>();
    }
}
