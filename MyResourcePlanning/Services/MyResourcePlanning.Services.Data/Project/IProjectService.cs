namespace MyResourcePlanning.Services.Data.Project
{
    using System.Collections.Generic;

    public interface IProjectService
    {
        IEnumerable<TViewModel> GetAllProjects<TViewModel>();
    }
}
