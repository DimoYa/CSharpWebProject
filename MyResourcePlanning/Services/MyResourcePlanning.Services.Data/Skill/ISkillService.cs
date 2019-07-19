namespace MyResourcePlanning.Services.Data.Skill
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Web.BindingModels.Skill;

    public interface ISkillService
    {
        Task<IEnumerable<TViewModel>> GetAllSkillsByCategories<TViewModel>();

        Task<IEnumerable<TViewModel>> GetAllSkillsCategories<TViewModel>();

        Task<bool> CreateCategory(SkillCategoryCreateBindingModel model);

        Task<bool> Create(SkillCreateBaseModel model);
    }
}
