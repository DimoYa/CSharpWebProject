namespace MyResourcePlanning.Services.Data.Project
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Web.BindingModels.Project;

    public interface IProjectService
    {
        Task<IEnumerable<TViewModel>> GetAllProjects<TViewModel>();

        Task<bool> Create(ProjectCreateBindingModel inputModel);

        Task<bool> DeleteById(string id);
    }
}
