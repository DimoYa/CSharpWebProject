namespace MyResourcePlanning.Services.Data.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Calendar;

    public class CalendarService : ICalendarService
    {
        private readonly MyResourcePlanningDbContext context;

        public CalendarService(MyResourcePlanningDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CheckIfPeriodExist(CalnedarCreatePeriodBindingModel model)
        {
            var startDate = model.StartDate;
            var endDate = model.EndDate;

            var daysToAdd = await this.GetBusinessDatesBetween(startDate, endDate);

            var daysInDB = this.context.Calendars
               .Where(c => c.Day >= startDate && c.Day <= endDate)
               .Select(c => c.Day)
               .ToList();

            var result = daysInDB.Intersect(daysToAdd).Any();

            return result;
        }

        public async Task<bool> CreatePeriod(CalnedarCreatePeriodBindingModel inputModel)
        {
            var startDate = inputModel.StartDate;
            var endDate = inputModel.EndDate;
            bool isHoliday = inputModel.IspublicHoliday;

            var daysToAdd = await this.GetBusinessDatesBetween(startDate, endDate);

            var calendarDaysToadd = new List<Calendar>();

            foreach (var day in daysToAdd)
            {
                Calendar calendar = new Calendar
                {
                    Day = day,
                    IsPublicHoliday = isHoliday,
                };

                calendarDaysToadd.Add(calendar);
            }

            this.context.Calendars.AddRange(calendarDaysToadd);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<TViewModel>> GetAllDays<TViewModel>(CalnedarViewPeriodBindingModel model)
        {
            var calendaDays = this.context.Calendars
                .Where(c => c.Day >= model.StartDate && c.Day <= model.EndDate)
                .To<TViewModel>()
                .ToList();

            return calendaDays;
        }

        private Task<List<DateTime>> GetBusinessDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.Date.DayOfWeek == DayOfWeek.Saturday
                    || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }
                else
                {
                    allDates.Add(date.Date);
                }
            }

            return Task.FromResult(allDates);
        }
    }
}
