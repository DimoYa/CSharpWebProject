namespace MyResourcePlanning.Web.BindingModels.Skill
{
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Models;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;

    public class SkillEditBindingModel : IHaveCustomMappings
        {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Skill Category")]
        public SkillCategory SkillCategory { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Skill, SkillEditBindingModel>()
                .ForMember(
                    s => s.Name,
                    opt => opt.MapFrom(s => s.Name))
                .ForMember(
                    e => e.SkillCategory,
                    opt => opt.MapFrom(e => e.SkillCategory));
        }
    }
}
