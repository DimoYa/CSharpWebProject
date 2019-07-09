namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.Collections.Generic;
    using MyResourcePlanning.Data.Models.Enums;

    public class Training
    {
        public Training()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserTrainings>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public TrainingType Type { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<UserTrainings> Users { get; set; }

    }
}
