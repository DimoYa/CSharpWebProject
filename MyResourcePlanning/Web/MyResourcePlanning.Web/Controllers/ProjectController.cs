namespace MyResourcePlanning.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Common;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Web.ViewModels.Project;

    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [Authorize(Roles =
            GlobalConstants.PlannerRoleName + "," +
            GlobalConstants.ApproverRoleName + "," +
            GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> All()
        {
            var requests = await this.projectService.GetAllProjects<ProjectAllViewModel>();
            return this.View(requests);
        }
    }
}
