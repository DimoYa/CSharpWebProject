namespace MyResourcePlanning.Models
{
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Models.Enums;

    public class UserCalendar
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string CalendarId { get; set; }

        public Calendar Calendar { get; set; }

        [Required]
        public UserCalendarAbsenceType AbsenceType { get; set; }
    }
}
