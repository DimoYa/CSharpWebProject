namespace MyResourcePlanning.Web.ViewModels.Skill
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class SkillsByCategoryViewModel : IMapFrom<SkillCategory>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SkillCategory, SkillsByCategoryViewModel>()
               .ForMember(
                   s => s.Skills,
                   opt => opt.MapFrom(s => s.Skills.Where(x => x.IsDeleted == false).ToList()));
        }
    }
}
