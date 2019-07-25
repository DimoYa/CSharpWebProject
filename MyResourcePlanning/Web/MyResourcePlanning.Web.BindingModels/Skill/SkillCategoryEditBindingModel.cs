namespace MyResourcePlanning.Web.BindingModels.Skill
{
    using System.ComponentModel.DataAnnotations;
    public class SkillCategoryEditBindingModel
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Skill category name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
