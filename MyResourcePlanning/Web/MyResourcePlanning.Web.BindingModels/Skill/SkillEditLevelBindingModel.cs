using MyResourcePlanning.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyResourcePlanning.Web.BindingModels.Skill
{
    public class SkillEditLevelBindingModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(SkillLevel))]
        [Display(Name = "Skill Level")]
        public SkillLevel SkillLevel { get; set; }
    }
}
