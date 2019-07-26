﻿namespace MyResourcePlanning.Models
{
    using MyResourcePlanning.Models.Enums;

    public class UserTraining
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string TrainingId { get; set; }

        public UserTrainingStatus Training { get; set; }

        public UserTrainingStatus Status { get; set; }
    }
}
