namespace MyResourcePlanning.Web.ViewModels.Training
{
    using System.Globalization;

    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class TrainingUserViewModel : IMapFrom<UserTraining>, IHaveCustomMappings
    {
        public string TrainingId { get; set; }

        public string TrainingName { get; set; }

        public string TrainingType { get; set; }

        public string TrainingStatus { get; set; }

        public string TrainingDueDate { get; set; }

        public string Status { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserTraining, TrainingUserViewModel>()
               .ForMember(
                   d => d.TrainingDueDate,
                   opt => opt.MapFrom(d => d.Training.DueDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
