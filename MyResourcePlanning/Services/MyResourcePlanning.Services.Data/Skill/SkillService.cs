namespace MyResourcePlanning.Services.Data.Skill
{
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

        public async Task<IEnumerable<TViewModel>> GetAllSkillsByCategories<TViewModel>()
        {
            var skillsByCategories = this.context.SkillCategories
                 .To<TViewModel>()
                 .ToList();

            return skillsByCategories;
        }

        public async Task<IEnumerable<TViewModel>> GetAllSkillsCategories<TViewModel>()
        {
            var skillsCategories = this.context.SkillCategories
                 .To<TViewModel>()
                 .ToList();

            return skillsCategories;
        }
    }
}
