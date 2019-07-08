namespace MyResourcePlanning.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Project
    {
        public Project()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Requests = new HashSet<Request>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
