namespace MyResourcePlanning.Models
{
    public class UserCalendar
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string CalendarId { get; set; }

        public Calendar Calendar { get; set; }

        public bool IsVacation { get; set; }
    }
}
