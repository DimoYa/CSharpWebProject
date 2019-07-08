namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Calendar
    {
        public Calendar()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserCalendar>();
        }

        public string Id { get; set; }

        [Required]
        public DateTime Day { get; set; }

        [Required]
        public bool IsPublicHoliday { get; set; }

        public virtual ICollection<UserCalendar> Users { get; set; }
    }
}
