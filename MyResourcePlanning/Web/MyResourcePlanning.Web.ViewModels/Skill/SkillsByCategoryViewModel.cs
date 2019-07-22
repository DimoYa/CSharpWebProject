namespace MyResourcePlanning.Web.ViewModels.Skill
{
    using System.Collections.Generic;

    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class SkillsByCategoryViewModel : IMapFrom<SkillCategory>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
