namespace MyResourcePlanning.Web.ViewModels.Project
{
    using System.Globalization;

    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class ProjectAllViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public double RemainingHours { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, ProjectAllViewModel>()
                .ForMember(
                    s => s.StartDate,
                    opt => opt.MapFrom(s => s.StartDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)))
                .ForMember(
                    e => e.EndDate,
                    opt => opt.MapFrom(e => e.EndDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)))
                .ForMember(
                    w => w.RemainingHours,
                    opt => opt.MapFrom(e => e.RequestedHours));
        }
    }
}
