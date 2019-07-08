﻿namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using MyResourcePlanning.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.CalendarDays = new HashSet<UserCalendar>();
            this.Trainings = new HashSet<UserTrainings>();
            this.Skills = new HashSet<UserSkills>();
            this.Requests = new HashSet<Request>();
        }

        [Required]
        [RegularExpression("[A-Z][a-z]+", ErrorMessage = "{0} should contain only letters starting with a capital case")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("[A-Z][a-z]+", ErrorMessage = "{0} should contain only letters starting with a capital case")]
        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string ApproverId { get; set; }

        public ApplicationUser Approver { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<UserCalendar> CalendarDays { get; set; }

        public virtual ICollection<UserTrainings> Trainings { get; set; }

        public virtual ICollection<UserSkills> Skills { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
