namespace MyResourcePlanning.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Skill;
    using MyResourcePlanning.Services.Data.SkillCategory;
    using MyResourcePlanning.Web.BindingModels.Skill;

    public class SkillController : AdminController
    {
        private readonly ISkillService skillService;
        private readonly ISkillCategoryService skillCategoryService;

        public SkillController(
            ISkillService skillService,
            ISkillCategoryService skillCategoryService)
        {
            this.skillService = skillService;
            this.skillCategoryService = skillCategoryService;
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

            await this.skillCategoryService.CreateCategory(inputModel);

            return this.Redirect("/Skill/All");
        }

        public async Task<IActionResult> EditCategory(string id)
        {
            var categoryForUpdate = await this.skillCategoryService.GetCategoryById(id);

            return this.View(categoryForUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(SkillCategoryEditBindingModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model ?? new SkillCategoryEditBindingModel());
            }

            await this.skillCategoryService.EditCategory(model, id);

            return this.Redirect("/Skill/All");
        }

        public async Task<IActionResult> DeleteCategory(string id)
        {
            await this.skillCategoryService.DeleteCategory(id);
            return this.Redirect("/Skill/All");
        }

        public async Task<IActionResult> CreateSkill()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill(SkillCreateBindingModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new SkillCreateBindingModel());
            }

            await this.skillService.CreateSkill(inputModel);

            return this.Redirect("/Skill/All");
        }

        public async Task<IActionResult> EditSkill(string id)
        {
            var skillForUpdate = await this.skillService.GetSkillById(id);
            var skillEditBaseModel = await this.skillService.GetSkillEditBaseModel(skillForUpdate);

            return this.View(skillEditBaseModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditSkill(SkillEditBindingModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model ?? new SkillEditBindingModel());
            }

            await this.skillService.EditSkill(model, id);

            return this.Redirect("/Skill/All");
        }

        public async Task<IActionResult> DeleteSkill(string id)
        {
            await this.skillService.DeleteSkill(id);
            return this.Redirect("/Skill/All");
        }
    }
}
