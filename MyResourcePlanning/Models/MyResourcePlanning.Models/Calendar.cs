namespace MyResourcePlanning.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Models.BaseModels;
    using MyResourcePlanning.Web.Infrastructure.Validators;

    public class Calendar : BaseModel<string>
    {
        public Calendar()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserCalendar>();
        }

        [Required]
        [DateGreaterOrEqualThanPresent]
        public DateTime Day { get; set; }

        [Required]
        public bool IsPublicHoliday { get; set; }

        public virtual ICollection<UserCalendar> Users { get; set; }
    }
}
