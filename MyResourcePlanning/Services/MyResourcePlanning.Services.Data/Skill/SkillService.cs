namespace MyResourcePlanning.Services.Data.Skill
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Data.SkillCategory;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Skill;

    public class SkillService : ISkillService
    {
        private readonly MyResourcePlanningDbContext context;
        private readonly ISkillCategoryService skillCategoryService;
        private readonly IUserService userService;

        public SkillService(
            MyResourcePlanningDbContext context,
            ISkillCategoryService skillCategoryService,
            IUserService userService)
        {
            this.context = context;
            this.skillCategoryService = skillCategoryService;
            this.userService = userService;
        }

        public async Task<bool> CreateSkill(SkillCreateBaseModel model)
        {
            var skillCategory = await this.skillCategoryService.GetCategoryByName(model.BindingModel.SkillCategory);

            Skill skill = new Skill
            {
                Name = model.BindingModel.Name,
                SkillCategory = skillCategory,
            };

            this.context.Skills.Add(skill);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditSkill(SkillEditBindingModel model, string id)
        {
            var skillForUpdate = this.context
                .Skills
                .SingleOrDefault(s => s.Id == id);

            var skillCategory = await this.skillCategoryService.GetCategoryByName(model.SkillCategory);

            skillForUpdate.Name = model.Name;
            skillForUpdate.SkillCategory = skillCategory;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteSkill(string id)
        {
            var skillForDeletion = this.context.Skills
                .SingleOrDefault(s => s.Id == id);

            skillForDeletion.IsDeleted = true;
            skillForDeletion.DeletedOn = DateTime.UtcNow;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddSkill(string id, SkillAddBindingModel model)
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            UserSkill userSkill = new UserSkill
            {
                UserId = currentUserId,
                SkillId = id,
                Level = model.SkillLevel,
            };

            this.context.UserSkills.Add(userSkill);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<TViewModel>> GetAllSkillsByCategories<TViewModel>()
        {
            var skillsByCategories = this.context.SkillCategories
                 .Where(s => s.IsDeleted == false)
                 .OrderBy(x => x.Name)
                 .To<TViewModel>()
                 .ToList();

            return skillsByCategories;
        }

        public async Task<Skill> GetSkillById(string id)
        {
            var skillForUpdate = this.context.Skills
                  .Where(s => s.Id == id)
                  .SingleOrDefault();

            var skillCategory = this.context
                .SkillCategories
                .SingleOrDefault(s => s.Id == skillForUpdate.SkillCategoryId);

            skillForUpdate.SkillCategory = skillCategory;

            return skillForUpdate;
        }

        public async Task<IList<string>> UserSkillsId()
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            var userSkillsId = this.context.UserSkills
                .Where(u => u.UserId == currentUserId)
                .Select(s => s.SkillId)
                .ToList();

            return userSkillsId;
        }
    }
}
