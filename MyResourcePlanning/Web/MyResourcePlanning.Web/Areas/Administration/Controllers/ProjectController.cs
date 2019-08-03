namespace MyResourcePlanning.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Web.BindingModels.Project;
    using MyResourcePlanning.Web.ViewModels.Project;

    public class ProjectController : AdminController
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet("/Administration/Project/Create")]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost("/Administration/Project/Create")]
        public async Task<IActionResult> Create(ProjectCreateBindingModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new ProjectCreateBindingModel());
            }

            await this.projectService.Create(inputModel);

            return this.Redirect("/Project/All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var projectForUpdate = await this.projectService.MapProject<ProjectAllViewModel>(id);

            return this.View(projectForUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectEditBindingModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(new ProjectAllViewModel());
            }

            await this.projectService.Edit(model, id);

            return this.Redirect("/Project/All");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.projectService.Delete(id);

            return this.Redirect("/Project/All");
        }
    }
}
