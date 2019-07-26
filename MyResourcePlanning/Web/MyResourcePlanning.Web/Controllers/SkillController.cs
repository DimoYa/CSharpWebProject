namespace MyResourcePlanning.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Skill;
    using MyResourcePlanning.Services.Data.SkillCategory;
    using MyResourcePlanning.Web.BindingModels.Skill;
    using MyResourcePlanning.Web.ViewModels.Skill;

    public class SkillController : BaseController
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

            return this.RedirectToAction(nameof(this.All));
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

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> DeleteCategory(string id)
        {
            await this.skillCategoryService.DeleteCategory(id);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> CreateSkill()
        {
            var skillCreateBaseModel = await this.skillService.GetSkillCreateBaseModel();

            return this.View(skillCreateBaseModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill(SkillCreateBaseModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new SkillCreateBaseModel());
            }

            await this.skillService.CreateSkill(inputModel);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> EditSkill(string id)
        {
            var skillForUpdate = await this.skillService.GetSkillById(id);
            var skillEdutBaseModel = await this.skillService.GetSkillEditBaseModel(skillForUpdate);

            return this.View(skillEdutBaseModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditSkill(SkillEditBindingModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model ?? new SkillEditBindingModel());
            }

            await this.skillService.EditSkill(model, id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> DeleteSkill(string id)
        {
            await this.skillService.DeleteSkill(id);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> AddSkill(string id)
        {
            var skillToAdd = await this.skillService.GetSkillById(id);

            var baseAddSkillModel = await this.skillService.GetSkillAddBaseModel(skillToAdd);

            return this.View(baseAddSkillModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill(SkillAddBindingModel inputModel, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new SkillAddBindingModel());
            }

            await this.skillService.AddSkillToMyProfile(id, inputModel);

            return this.RedirectToAction(nameof(this.MySkills));
        }

        public async Task<IActionResult> RemoveSkill(string id)
        {
            await this.skillService.RemoveSkillFromProfile(id);
            return this.RedirectToAction(nameof(this.MySkills));
        }

        public async Task<IActionResult> EditSkilllevel(string id)
        {
            var skillForUpdate = await this.skillService.GetCurrentuserSkillById(id);
            var skillEditBaseModel = await this.skillService.GetSkillEditLevelBaseModel(skillForUpdate);

            return this.View(skillEditBaseModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditSkilllevel(SkillEditLevelBindingModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model ?? new SkillEditLevelBindingModel());
            }

            await this.skillService.EditSkillLevel(model, id);

            return this.RedirectToAction(nameof(this.MySkills));
        }

        public async Task<IActionResult> All()
        {
            var skills = await this.skillService
                .GetAllSkillsByCategories<SkillsByCategoryViewModel>();

            var currentUserSkills = await this.skillService.GetUserSkillsId();

            return this.View(Tuple.Create(skills.ToList(), currentUserSkills));
        }

        public async Task<IActionResult> MySkills()
        {
            var userSkills = await this.skillService
                .GetUserSkillsByCategories<UserSkillsByCategoryViewModel>();

            var groupedUserSkillInfo = userSkills.GroupBy(u => u.SkillCategoryName)
                                      .Select(grp => new GroupedUserSkillsViewModel
                                      {
                                          CategoryName = grp.Key,
                                          SkillInfo = grp.ToList(),
                                      })
                                      .ToList();

            return this.View(groupedUserSkillInfo);
        }
    }
}
