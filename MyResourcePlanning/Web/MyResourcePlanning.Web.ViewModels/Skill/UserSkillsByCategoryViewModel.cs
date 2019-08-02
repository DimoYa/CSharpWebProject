namespace MyResourcePlanning.Web.ViewModels.Skill
{
    using AutoMapper;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class UserSkillsByCategoryViewModel : IMapFrom<UserSkill>, IHaveCustomMappings
    {
        public string SkillCategoryName { get; set; }

        public string SkillId { get; set; }

        public string SkillName { get; set; }

        public string Level { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserSkill, UserSkillsByCategoryViewModel>()
               .ForMember(
                   s => s.SkillCategoryName,
                   opt => opt.MapFrom(c => c.Skill.SkillCategory.Name));
        }
    }
}
