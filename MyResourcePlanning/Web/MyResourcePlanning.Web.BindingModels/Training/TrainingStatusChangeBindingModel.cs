namespace MyResourcePlanning.Web.BindingModels.Training
{
    using AutoMapper;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Models;
    using System.Globalization;
    using MyResourcePlanning.Models.Enums;

    public class TrainingStatusChangeBindingModel : IMapFrom<UserTraining>, IHaveCustomMappings
    {
        public string TrainingId { get; set; }

        public string TrainingName { get; set; }

        public string TrainingType { get; set; }

        public string TrainingStatus { get; set; }

        public string TrainingDueDate { get; set; }

        public UserTrainingStatus Status { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserTraining, TrainingStatusChangeBindingModel>()
               .ForMember(
                   d => d.TrainingDueDate,
                   opt => opt.MapFrom(d => d.Training.DueDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)));
        }
    }
}


