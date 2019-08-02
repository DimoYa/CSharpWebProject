namespace MyResourcePlanning.Web.ViewModels.Calendar
{
    using System.Globalization;

    using AutoMapper;
    using MyResourcePlanning.Services.Mapping;

    using Calendar = MyResourcePlanning.Models.Calendar;

    public class CalendarAllViewModel : IMapFrom<Calendar>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Day { get; set; }

        public bool IspublicHoliday { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Calendar, CalendarAllViewModel>()
                .ForMember(
                    c => c.Day,
                    opt => opt.MapFrom(c => c.Day.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
