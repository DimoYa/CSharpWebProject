namespace MyResourcePlanning.Web.ViewModels.Skill
{
    using System.Collections.Generic;

    public class GroupedUserSkillsViewModel
    {
        public string CategoryName { get; set; }

        public List<UserSkillsByCategoryViewModel> SkillInfo { get; set; }
    }
}
