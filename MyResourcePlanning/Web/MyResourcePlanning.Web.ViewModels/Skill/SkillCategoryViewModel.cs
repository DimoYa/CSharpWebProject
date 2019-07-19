namespace MyResourcePlanning.Web.ViewModels.Skill
{
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class SkillCategoryViewModel : IMapFrom<SkillCategory>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
