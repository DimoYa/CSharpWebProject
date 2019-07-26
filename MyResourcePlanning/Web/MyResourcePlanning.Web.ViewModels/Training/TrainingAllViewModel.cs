namespace MyResourcePlanning.Web.ViewModels.Training
{
    using System.Globalization;

    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class TrainingAllViewModel : IMapFrom<Training>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string DueDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Training, TrainingAllViewModel>()
               .ForMember(
                   d => d.DueDate,
                   opt => opt.MapFrom(d => d.DueDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
