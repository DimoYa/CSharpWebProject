namespace MyResourcePlanning.Data.Models
{
    using System.Collections.Generic;
    using MyResourcePlanning.Data.Models.Enums;

    public class UserTraining
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string TrainingId { get; set; }

        public Training Training { get; set; }

        public TrainingStatus Status { get; set; }
    }
}
