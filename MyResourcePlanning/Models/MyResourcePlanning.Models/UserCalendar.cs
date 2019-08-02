namespace MyResourcePlanning.Models
{
    using MyResourcePlanning.Models.Enums;

    public class UserCalendar
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string CalendarId { get; set; }

        public Calendar Calendar { get; set; }

        public UserCalendarAbsenceType AbsenceType { get; set; }
    }
}
