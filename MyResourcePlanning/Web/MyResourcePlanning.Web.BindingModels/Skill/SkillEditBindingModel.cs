namespace MyResourcePlanning.Web.BindingModels.Skill
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using MyResourcePlanning.Web.ViewModels.Skill;

    public class SkillEditBindingModel
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Skill Category")]
        public string SkillCategory { get; set; }

        public IEnumerable<SkillCategoryViewModel> SkillCategories { get; set; }

    }
}
