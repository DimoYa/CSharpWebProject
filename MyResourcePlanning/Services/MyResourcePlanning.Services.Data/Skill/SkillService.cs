namespace MyResourcePlanning.Services.Data.Skill
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<TViewModel>> GetUserSkillsByCategories<TViewModel>()
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            var skillsByCategories = this.context.UserSkills
                 .Include(us => us.Skill).ThenInclude(c=> c.SkillCategory)
                 .Where(us => us.UserId == currentUserId)
                 .Where(us => us.IsDeleted == false)
                 .To<TViewModel>()
                 .ToList();

            return skillsByCategories;
        }

        public async Task<Skill> GetSkillById(string id)
        {
            var skill = this.context.Skills
                  .Where(s => s.Id == id)
                  .SingleOrDefault();

            var skillCategory = this.context
                .SkillCategories
                .SingleOrDefault(s => s.Id == skill.SkillCategoryId);

            skill.SkillCategory = skillCategory;

            return skill;
        }

        public async Task<IList<string>> GetUserSkillsId()
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
