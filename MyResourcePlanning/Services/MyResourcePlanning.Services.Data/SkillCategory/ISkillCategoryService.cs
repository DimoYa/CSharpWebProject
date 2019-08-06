namespace MyResourcePlanning.Services.Data.SkillCategory
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Models;
    using MyResourcePlanning.Web.BindingModels.Skill;

    public interface ISkillCategoryService
    {
        Task<bool> CreateCategory(SkillCategoryCreateBindingModel model);

        Task<bool> EditCategory(SkillCategoryEditBindingModel model, string id);

        Task<bool> DeleteCategory(string id);

        Task<SkillCategory> GetCategoryByName(string name);

        Task<SkillCategory> GetCategoryById(string id);

        Task<IEnumerable<TViewModel>> GetAllActiveSkillCategories<TViewModel>();
    }
}
