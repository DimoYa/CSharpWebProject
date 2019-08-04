namespace MyResourcePlanning.Web.BindingModels.Skill
{
    using MyResourcePlanning.Models.Enums;
    using MyResourcePlanning.Models;
    using System.ComponentModel.DataAnnotations;
    public class SkillAddBindingModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(SkillLevel))]
        [Display(Name = "Skill Level")]
        public SkillLevel SkillLevel { get; set; }

        [Required]
        public Skill SkillToAdd { get; set; }

    }
}
