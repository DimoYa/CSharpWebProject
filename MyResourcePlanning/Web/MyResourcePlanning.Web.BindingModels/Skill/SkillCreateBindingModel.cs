namespace MyResourcePlanning.Web.BindingModels.Skill
{
    using System.ComponentModel.DataAnnotations;
    public class SkillCreateBindingModel
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
