namespace MyResourcePlanning.Web.BindingModels.Skill
{
    using MyResourcePlanning.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class SkillEditLevelBindingModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(SkillLevel))]
        [Display(Name = "Skill Level")]
        public SkillLevel SkillLevel { get; set; }
    }
}
