namespace MyResourcePlanning.Web.ViewModels.Request
{
    using System;
    using System.Globalization;

    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class RequestAllViewModel : IMapFrom<Request>, IHaveCustomMappings
    {
        public string Project { get; set; }

        public string Resource { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string WorkingHours { get; set; }

        public string Status { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Request, RequestAllViewModel>()
                .ForMember(
                    s => s.StartDate,
                    opt => opt.MapFrom(s => s.StartDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)))
                .ForMember(
                    e => e.EndDate,
                    opt => opt.MapFrom(e => e.EndDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)))
                 .ForMember(
                    p => p.Project,
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
