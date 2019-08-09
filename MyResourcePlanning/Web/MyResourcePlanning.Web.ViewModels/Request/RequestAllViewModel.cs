namespace MyResourcePlanning.Web.ViewModels.Request
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.Infrastructure.Validators;

    public class RequestAllViewModel : IMapFrom<Request>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ProjectName { get; set; }

        public string Resource { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Required]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be greater than Start Date")]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please input positive hours")]
        [Display(Name = "Working Hours")]
        public string WorkingHours { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Request, RequestAllViewModel>()
                .ForMember(
                    s => s.StartDate,
                    opt => opt.MapFrom(s => s.StartDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(
                    e => e.EndDate,
                    opt => opt.MapFrom(e => e.EndDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                 .ForMember(
                    p => p.ProjectName,
                    opt => opt.MapFrom(p => p.Project.Name))
                 .ForMember(
                    u => u.Resource,
                    opt => opt.MapFrom(u => $"{u.User.FirstName} {u.User.LastName}"))
                 .ForMember(
                    w => w.WorkingHours,
                    opt => opt.MapFrom(w => w.WorkingHours.ToString("F2")));
        }
    }
}
