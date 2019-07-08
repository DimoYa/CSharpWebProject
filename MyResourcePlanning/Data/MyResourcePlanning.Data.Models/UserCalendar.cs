namespace MyResourcePlanning.Data.Models
{
    public class UserCalendar
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string CalendarID { get; set; }

        public Calendar Calendar { get; set; }

        public bool IsVacation { get; set; }
    }
}
