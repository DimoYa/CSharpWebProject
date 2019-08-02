namespace MyResourcePlanning.Web.ViewModels.Calendar
{
    using System.Globalization;

    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Models.Enums;
    using MyResourcePlanning.Services.Mapping;

    public class CalendarMyViewModel : IMapFrom<UserCalendar>, IHaveCustomMappings
    {
        public string CalendarId { get; set; }

        public string Day { get; set; }

        public UserCalendarAbsenceType AbsenceType { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserCalendar, CalendarMyViewModel>()
                .ForMember(
                    c => c.Day,
                    opt => opt.MapFrom(c => c.Calendar.Day.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
