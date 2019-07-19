using System.ComponentModel.DataAnnotations;

namespace MyResourcePlanning.Web.BindingModels.Skill
{
    public class SkillCategoryCreateBindingModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Skill Level")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
