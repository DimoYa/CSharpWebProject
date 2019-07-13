namespace MyResourcePlanning.Services
{
    using System.Collections.Generic;

    public interface IProjectService
    {
        IEnumerable<TViewModel> GetAllProjects<TViewModel>();
    }
}
