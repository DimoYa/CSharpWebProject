namespace MyResourcePlanning.Models
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using MyResourcePlanning.Models.BaseModels;

    public class UserRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public UserRole()
            : this(null)
        {
        }

        public UserRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
