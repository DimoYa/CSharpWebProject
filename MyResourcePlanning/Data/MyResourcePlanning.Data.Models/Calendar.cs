namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Calendar
    {
        public Calendar()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserCalendar>();
        }

        public string Id { get; set; }

        public DateTime Day { get; set; }

        public bool IsPublicHoliday { get; set; }

        public virtual ICollection<UserCalendar> Users { get; set; }
    }
}
