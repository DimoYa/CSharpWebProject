namespace MyResourcePlanning.Web.ViewModels.Training
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class TrainingAllUsersView : IMapFrom<UserTraining>, IHaveCustomMappings
    {
        public string TrainingId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Employee")]
        public string UserName { get; set; }

        [Display(Name = "Training")]
        public string TrainingName { get; set; }

        [Display(Name = "Type")]
        public string TrainingType { get; set; }

        [Display(Name = "Status")]
        public string TrainingStatus { get; set; }

        [Display(Name = "Due date")]
        public string TrainingDueDate { get; set; }

        [Required]
        [Display(Name = "State")]
        public string Status { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserTraining, TrainingAllUsersView>()
               .ForMember(
                   d => d.TrainingDueDate,
                   opt => opt.MapFrom(d => d.Training.DueDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)))
               .ForMember(
                   u => u.UserName,
                   opt => opt.MapFrom(u => $"{u.User.FirstName} {u.User.LastName}"));
        }
    }
}
