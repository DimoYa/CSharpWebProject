namespace MyResourcePlanning.Web.ViewModels.User
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class UsersViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Skills { get; set; }

        public List<string> Trainings { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UsersViewModel>()
                .ForMember(
                    u => u.Skills,
                    opt => opt.MapFrom(u => u.Skills.Select(s => s.Skill.Name).ToList()))
                .ForMember(
                    u => u.Trainings,
                    opt => opt.MapFrom(u => u.Trainings.Select(t => t.Training.Name).ToList()));
        }
    }
}
