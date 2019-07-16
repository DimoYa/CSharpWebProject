namespace MyResourcePlanning.Services.Data.Project
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Web.ViewModels.Project;

    public interface IProjectService
    {
        Task<IEnumerable<TViewModel>> GetAllProjects<TViewModel>();

        Task<bool> Create(ProjectCreateInputModel inputModel);
    }
}
