namespace MyResourcePlanning.Data.Models
{
    using System;
    using MyResourcePlanning.Data.Models.Enums;

    public class Request
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double WorkingHours { get; set; }

        public RequestStatus Status { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
