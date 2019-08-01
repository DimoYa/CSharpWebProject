﻿namespace MyResourcePlanning.Services.Data.SkillCategory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Web.BindingModels.Skill;
    using MyResourcePlanning.Services.Mapping;


    public class SkillCategoryService : ISkillCategoryService
    {
        private readonly MyResourcePlanningDbContext context;

        public SkillCategoryService(MyResourcePlanningDbContext context)
        {
            this.context = context;
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

        public async Task<bool> EditCategory(SkillCategoryEditBindingModel model, string id)
        {
            var categoryForUpdate = await this.GetCategoryById(id);

            categoryForUpdate.Name = model.Name;

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

        public async Task<SkillCategory> GetCategoryByName(string categoryName)
        {
            var category = this.context
                               .SkillCategories
                               .Where(s => s.IsDeleted == false)
                               .SingleOrDefault(s => s.Name == categoryName);

            return category;
        }

        public async Task<SkillCategory> GetCategoryById(string id)
        {
            var category = this.context
                              .SkillCategories
                              .Where(s => s.IsDeleted == false)
                              .SingleOrDefault(s => s.Id == id);

            return category;
        }

        public async Task<IEnumerable<TViewModel>> GetAllSkillCategories<TViewModel>()
        {
            var categories = this.context
                              .SkillCategories
                              .Where(s => s.IsDeleted == false)
                              .To<TViewModel>()
                              .ToList();

            return categories;
        }
    }
}
