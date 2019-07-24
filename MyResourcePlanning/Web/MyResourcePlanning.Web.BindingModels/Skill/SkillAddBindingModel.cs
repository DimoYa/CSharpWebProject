using MyResourcePlanning.Models;
using MyResourcePlanning.Models.Enums;
using MyResourcePlanning.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyResourcePlanning.Web.BindingModels.Skill
{
    public class SkillAddBindingModel : IMapFrom<UserSkill>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select skill level")]
        [Display(Name = "Skill Level")]
        public SkillLevel SkillLevel { get; set; }

    }
}
