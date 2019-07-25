namespace MyResourcePlanning.Services.Data.Skill
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Models;
    using MyResourcePlanning.Web.BindingModels.Skill;

    public interface ISkillService
    {
        Task<IEnumerable<TViewModel>> GetAllSkillsByCategories<TViewModel>();

        Task<IEnumerable<TViewModel>> GetUserSkillsByCategories<TViewModel>();

        Task<bool> CreateSkill(SkillCreateBaseModel model);

        Task<bool> EditSkill(SkillEditBindingModel model, string id);

        Task<bool> DeleteSkill(string id);

        Task<bool> AddSkillToMyProfile(string id, SkillAddBindingModel model);

        Task<bool> RemoveSkillFromProfile(string id);

        Task<Skill> GetSkillById(string id);

        Task<UserSkill> GetCurrentuserSkillById(string id);

        Task<bool> EditSkillLevel(SkillEditLevelBindingModel model, string id);

        Task<IList<string>> GetUserSkillsId();

        Task<SkillCreateBaseModel> GetSkillCreateBaseModel();

        Task<SkillEditBindingModel> GetSkillEditBaseModel(Skill skillForUpdate);

        Task<SkillEditLevelBindingModel> GetSkillEditLevelBaseModel(UserSkill skillForUpdate);

        Task<SkillAddBindingModel> GetSkillAddBaseModel(Skill skillToAdd);
    }
}
