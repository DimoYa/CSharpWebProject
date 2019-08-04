namespace MyResourcePlanning.Models
{
    using System.ComponentModel.DataAnnotations;
    using MyResourcePlanning.Models.Enums;

    public class UserTraining
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string TrainingId { get; set; }

        public Training Training { get; set; }

        [Required]
        public UserTrainingStatus Status { get; set; }
    }
}
