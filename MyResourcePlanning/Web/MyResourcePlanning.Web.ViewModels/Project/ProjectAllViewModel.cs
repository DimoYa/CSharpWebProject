namespace MyResourcePlanning.Web.ViewModels.Project
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.Infrastructure.Validators;

    public class ProjectAllViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Text)]
        public string StartDate { get; set; }

        [Required]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be greater than Start Date")]
        [Display(Name = "End Date")]
        [DataType(DataType.Text)]
        public string EndDate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please Enter positive number")]
        [Display(Name = "Requested Hours")]
        public string RequestedHours { get; set; }

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
                    w => w.RequestedHours,
                    opt => opt.MapFrom(e => e.RequestedHours.ToString("F2")));
        }
    }
}
