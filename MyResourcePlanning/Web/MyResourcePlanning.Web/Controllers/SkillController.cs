namespace MyResourcePlanning.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Skill;
    using MyResourcePlanning.Web.BindingModels.Skill;
    using MyResourcePlanning.Web.ViewModels.Skill;

    public class SkillController : BaseController
    {
        private readonly ISkillService skillService;

        public SkillController(ISkillService skillService)
        {
            this.skillService = skillService;
        }

        public async Task<IActionResult> CreateCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(SkillCategoryCreateBindingModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new SkillCategoryCreateBindingModel());
            }

            await this.skillService.CreateCategory(inputModel);

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> CreateSkill()
        {
            var skillCreateBaseModel = await this.GetSkillCreateBaseModel();

            return this.View(skillCreateBaseModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill(SkillCreateBaseModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new SkillCreateBaseModel());
            }

            await this.skillService.Create(inputModel);

            return this.RedirectToAction("All");
        }

        private async Task<SkillCreateBaseModel> GetSkillCreateBaseModel()
        {
            return new SkillCreateBaseModel()
            {
                SkillCategories = await this.skillService.GetAllSkillsCategories<SkillCategoryViewModel>(),
            };
        }
    }
}
