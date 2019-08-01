namespace MyResourcePlanning.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Web.ViewModels.Project;

    [ViewComponent(Name = "ActiveProjects")]
    public class ActiveProjectsViewComponent : ViewComponent
    {
        private readonly IProjectService projectService;

        public ActiveProjectsViewComponent(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projects = await this.projectService.GetAllProjects<ProjectAllViewModel>();

            return this.View(projects);
        }
    }
}
