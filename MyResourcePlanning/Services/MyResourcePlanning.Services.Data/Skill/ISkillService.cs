namespace MyResourcePlanning.Services.Data.Skill
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Web.BindingModels.Skill;
    using MyResourcePlanning.Models;

    public interface ISkillService
    {
        Task<IEnumerable<TViewModel>> GetAllSkillsByCategories<TViewModel>();

        Task<bool> CreateCategory(SkillCategoryCreateBindingModel model);

        Task<bool> CreateSkill(SkillCreateBaseModel model);

        Task<bool> EditSkill(SkillEditBindingModel model, string id);

        Task<Skill> GetSkillById(string id);

        Task<bool> DeleteSkill(string id);

        Task<bool> DeleteCategory(string id);
    }
}
