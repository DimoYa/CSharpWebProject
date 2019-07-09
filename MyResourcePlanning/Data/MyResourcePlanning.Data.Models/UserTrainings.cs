namespace MyResourcePlanning.Data.Models
{
    using System.Collections.Generic;
    using MyResourcePlanning.Data.Models.Enums;

    public class UserTrainings
    {

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string TrainingID { get; set; }

        public Training Training { get; set; }

        public TrainingStatus Status { get; set; }

    }
}
