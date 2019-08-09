namespace MyResourcePlanning.Services.Data.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Calendar;

    public class CalendarService : ICalendarService
    {
        private readonly MyResourcePlanningDbContext context;
        private readonly IUserService userService;

        public CalendarService(
            MyResourcePlanningDbContext context,
            IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<bool> CheckIfPeriodExist(DateTime startDate, DateTime endDate)
        {
            var daysToAdd = await this.GetBusinessDatesBetween(startDate, endDate);

            var daysInDB = this.context.Calendars
               .Where(c => c.Day >= startDate && c.Day <= endDate)
               .Select(c => c.Day)
               .ToList();

            var result = daysInDB.Intersect(daysToAdd).Any();

            return result;
        }

        public async Task<bool> CheckIfAbsenceExistOrIsPublicHoliday(DateTime startDate, DateTime endDate)
        {
            var daysToAdd = await this.GetBusinessDatesBetween(startDate, endDate);

            var daysInDB = this.context.UserCalendars
               .Include(c => c.Calendar)
               .Where(c => c.Calendar.Day >= startDate && c.Calendar.Day <= endDate)
               .Select(c => c.Calendar.Day)
               .ToList();

            var publicHolidaysInDB = this.context.Calendars
               .Where(c => c.Day >= startDate && c.Day <= endDate && c.IsPublicHoliday == true)
               .Select(c => c.Day)
               .ToList();

            var isDayAlreadyAdded = daysInDB.Any();
            var isDayPublicHoliday = publicHolidaysInDB.Any();

            return isDayAlreadyAdded || isDayPublicHoliday;
        }

        public async Task<bool> CreatePeriod(CalendarCreatePeriodBindingModel inputModel)
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

        public async Task<bool> DeletePeriod(string id)
        {
            var calendarDayToDelete = await this.GetCalendarDayById(id);

            this.context.Calendars.Remove(calendarDayToDelete);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditPeriod(CalendarEditPeriodBindingModel model, string id)
        {
            var calendarDayToUpdate = await this.GetCalendarDayById(id);

            calendarDayToUpdate.IsPublicHoliday = model.IspublicHoliday;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> CreateAbsence(CalendarCreateAbsenceBindingModel inputModel)
        {
            var startDate = inputModel.StartDate;
            var endDate = inputModel.EndDate;
            var type = inputModel.AbsenceType;
            var currentUser = await this.userService.GetCurrentUserId();

            var daysToAdd = await this.GetBusinessDatesBetween(startDate, endDate);

            var userCalendarDaysToadd = new List<UserCalendar>();

            foreach (var day in daysToAdd)
            {
                UserCalendar userCalendar = new UserCalendar
                {
                    UserId = currentUser,
                    CalendarId = this.context.Calendars
                                             .SingleOrDefault(c => c.Day.ToString("dd/MM/yyyy")
                                             == day.ToString("dd/MM/yyyy")).Id,
                    AbsenceType = type,
                };

                userCalendarDaysToadd.Add(userCalendar);
            }

            this.context.UserCalendars.AddRange(userCalendarDaysToadd);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAbsence(string calendarId)
        {
            var userId = await this.userService.GetCurrentUserId();

            var entryToRemove = this.context.UserCalendars
                                            .SingleOrDefault(uc => uc.UserId == userId
                                            && uc.CalendarId == calendarId);

            this.context.UserCalendars.Remove(entryToRemove);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public Task<IEnumerable<TViewModel>> GetAllDays<TViewModel>()
        {
            var calendaDays = this.context.Calendars
                .OrderByDescending(c => c.Day)
                .To<TViewModel>()
                .ToList();

            return Task.FromResult(calendaDays.AsEnumerable());
        }

        public async Task<IEnumerable<TViewModel>> GetMyAbsenceDays<TViewModel>()
        {
            var currentUser = await this.userService.GetCurrentUserId();

            var calendaDays = this.context.UserCalendars
                .Include(c => c.Calendar)
                .Where(uc => uc.UserId == currentUser)
                .OrderByDescending(c => c.Calendar.Day)
                .To<TViewModel>()
                .ToList();

            return calendaDays;
        }

        public Task<TViewModel> MapPeriod<TViewModel>(string id)
        {
            var currentCalendar = this.context
              .Calendars
              .SingleOrDefault(p => p.Id == id)
              .To<TViewModel>();

            return Task.FromResult(currentCalendar);
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

        private Task<Calendar> GetCalendarDayById(string id)
        {
            var currentCalendar = this.context
               .Calendars
               .SingleOrDefault(p => p.Id == id);

            return Task.FromResult(currentCalendar);
        }
    }
}
