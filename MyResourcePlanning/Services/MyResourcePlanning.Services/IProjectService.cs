using MyResourcePlanning.Web.ViewModels.Project;
using System.Collections.Generic;

namespace MyResourcePlanning.Services
{
    public interface IProjectService
    {
        List<ProjectAllViewModel> GetAllProjects();
    }
}
