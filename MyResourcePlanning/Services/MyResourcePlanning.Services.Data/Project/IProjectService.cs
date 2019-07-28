namespace MyResourcePlanning.Services.Data.Project
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Models;
    using MyResourcePlanning.Web.BindingModels.Project;

    public interface IProjectService
    {
        Task<bool> Create(ProjectCreateBindingModel inputModel);

        Task<bool> Edit(ProjectEditBindingModel model, string id);

        Task<bool> Delete(string id);

        Task<Project> GetProjectById(string id);

        Task<Project> GetProjectByName(string name);

        Task<IEnumerable<TViewModel>> GetAllProjects<TViewModel>();

        Task<IEnumerable<TViewModel>> GetAllProjectsForRequest<TViewModel>();

        Task<TViewModel> MapProject<TViewModel>(string id);
    }
}
