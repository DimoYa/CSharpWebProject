namespace MyResourcePlanning.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Common;
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

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> AddSkill(string id)
        {
            var skillToAdd = await this.skillService.GetSkillById(id);

            var baseAddSkillModel = await this.skillService.GetSkillAddBaseModel(skillToAdd);

            return this.View(baseAddSkillModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> AddSkill(SkillAddBindingModel inputModel, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new SkillAddBindingModel());
            }

            await this.skillService.AddSkillToMyProfile(id, inputModel);

            return this.RedirectToAction(nameof(this.MySkills));
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> RemoveSkill(string id)
        {
            await this.skillService.RemoveSkillFromProfile(id);
            return this.RedirectToAction(nameof(this.MySkills));
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> EditSkilllevel(string id)
        {
            var skillForUpdate = await this.skillService.GetCurrentuserSkillById(id);
            var skillEditBaseModel = await this.skillService.GetSkillEditLevelBaseModel(skillForUpdate);

            return this.View(skillEditBaseModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> EditSkilllevel(SkillEditLevelBindingModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model ?? new SkillEditLevelBindingModel());
            }

            await this.skillService.EditSkillLevel(model, id);

            return this.RedirectToAction(nameof(this.MySkills));
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName + "," +
                           GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> All()
        {
            var skills = await this.skillService
                .GetAllSkillsByCategories<SkillsByCategoryViewModel>();

            var currentUserSkills = await this.skillService.GetCurrentUserSkillsId();

            return this.View(Tuple.Create(skills.ToList(), currentUserSkills));
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
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
