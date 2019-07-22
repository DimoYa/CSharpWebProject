namespace MyResourcePlanning.Web.BindingModels.Skill
{
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Models.Enums;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.ViewModels.Skill;
    using System.ComponentModel.DataAnnotations;
    public class SkillCreateBindingModel : IMapFrom<SkillCategory>
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Skill Category")]
        public string SkillCategory { get; set; }

    }
}
