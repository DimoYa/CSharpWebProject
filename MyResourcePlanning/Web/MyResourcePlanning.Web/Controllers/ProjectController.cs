namespace MyResourcePlanning.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Web.BindingModels.Project;
    using MyResourcePlanning.Web.ViewModels.Project;

    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateBindingModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new ProjectCreateBindingModel());
            }

            await this.projectService.Create(inputModel);

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            var requests = await this.projectService.GetAllProjects<ProjectAllViewModel>();
            return this.View(requests);
        }
    }
}
