namespace MyResourcePlanning.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.SkillCategory;
    using MyResourcePlanning.Web.ViewModels.Skill;

    [ViewComponent(Name = "ActiveSkillCategories")]
    public class ActiveSkillCategoriesViewComponent : ViewComponent
    {
        private readonly ISkillCategoryService skillCategoryService;

        public ActiveSkillCategoriesViewComponent(ISkillCategoryService skillCategoryService)
        {
            this.skillCategoryService = skillCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var skills = await this.skillCategoryService.GetAllSkillCategories<SkillCategoryViewModel>();

            return this.View(skills);
        }
    }
}