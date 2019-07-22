namespace MyResourcePlanning.Services.Data.Skill
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Skill;

    public class SkillService : ISkillService
    {
        private readonly MyResourcePlanningDbContext context;

        public SkillService(MyResourcePlanningDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(SkillCreateBaseModel model)
        {
            var skillCategoryID = this.context
                .SkillCategories
                .SingleOrDefault(s => s.Name == model.BindingModel.SkillCategory)
                .Id;

            Skill skill = new Skill
            {
                Name = model.BindingModel.Name,
                SkillCategoryId = skillCategoryID,
            };

            this.context.Skills.Add(skill);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> CreateCategory(SkillCategoryCreateBindingModel model)
        {
            SkillCategory skillCategory = new SkillCategory
            {
                Name = model.Name,
            };

            this.context.SkillCategories.Add(skillCategory);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteCategory(string id)
        {
            var skillCategoryForDeletion = this.context.SkillCategories
                .SingleOrDefault(s => s.Id == id);

            skillCategoryForDeletion.IsDeleted = true;
            skillCategoryForDeletion.DeletedOn = DateTime.UtcNow;

            var skillsUnderTheCategory = this.context.Skills
                .Where(s => s.SkillCategoryId == skillCategoryForDeletion.Id);

            foreach (var skill in skillsUnderTheCategory)
            {
                skill.IsDeleted = true;
                skill.DeletedOn = DateTime.UtcNow;
            }

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

        public async Task<IEnumerable<TViewModel>> GetAllSkillsByCategories<TViewModel>()
        {
            var skillsByCategories = this.context.SkillCategories
                 .Where(s => s.IsDeleted == false)
                 .OrderBy(x => x.Name)
                 .To<TViewModel>()
                 .ToList();

            return skillsByCategories;
        }
    }
}
