namespace MyResourcePlanning.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using MyResourcePlanning.Models.BaseModels;

    public class UserRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public UserRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
