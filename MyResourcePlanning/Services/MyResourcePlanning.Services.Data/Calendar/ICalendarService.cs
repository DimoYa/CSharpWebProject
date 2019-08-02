namespace MyResourcePlanning.Services.Data.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Web.BindingModels.Calendar;

    public interface ICalendarService
    {
        Task<bool> CreatePeriod(CalnedarCreatePeriodBindingModel inputModel);

        Task<bool> CheckIfPeriodExist(CalnedarCreatePeriodBindingModel inputModel);

        Task<bool> Edit(CalnedarEditPeriodBindingModel model, string id);

        Task<bool> Delete(string id);

        Task<TViewModel> MapPeriod<TViewModel>(string id);

        Task<IEnumerable<TViewModel>> GetAllDays<TViewModel>();
    }
}
