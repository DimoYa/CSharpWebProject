namespace MyResourcePlanning.Web.BindingModels.Skill
{
    using MyResourcePlanning.Web.ViewModels.Skill;
    using System.Collections.Generic;
    public class SkillCreateBaseModel
    {
        public SkillCreateBindingModel BindingModel { get; set; }

        public IEnumerable<SkillCategoryViewModel> SkillCategories { get; set; }
    }
}
