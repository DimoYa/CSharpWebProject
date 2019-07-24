namespace MyResourcePlanning.Services.Data.Skill
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Models;
    using MyResourcePlanning.Web.BindingModels.Skill;

    public interface ISkillService
    {
        Task<IEnumerable<TViewModel>> GetAllSkillsByCategories<TViewModel>();

        Task<bool> CreateSkill(SkillCreateBaseModel model);

        Task<bool> EditSkill(SkillEditBindingModel model, string id);

        Task<bool> DeleteSkill(string id);

        Task<bool> AddSkill(string id, SkillAddBindingModel model);

        Task<Skill> GetSkillById(string id);

        Task<IList<string>> UserSkillsId();
    }
}
