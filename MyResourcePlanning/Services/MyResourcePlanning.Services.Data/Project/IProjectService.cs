namespace MyResourcePlanning.Services.Data.Project
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProjectService
    {
        Task<IEnumerable<TViewModel>> GetAllProjects<TViewModel>();
    }
}
