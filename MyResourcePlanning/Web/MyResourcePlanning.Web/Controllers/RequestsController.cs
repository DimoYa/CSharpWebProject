namespace MyResourcePlanning.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Services.Data.Request;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Web.ViewModels.Project;
    using MyResourcePlanning.Web.ViewModels.Request;
    using MyResourcePlanning.Web.ViewModels.User;

    public class RequestsController : BaseController
    {
        private readonly IRequestService requestsService;
        private readonly IUserService userService;
        private readonly IProjectService projectService;

        public RequestsController(
            IRequestService requestsService,
            IUserService userService,
            IProjectService projectService)
        {
            this.requestsService = requestsService;
            this.userService = userService;
            this.projectService = projectService;
        }

        public async Task<IActionResult> Create()
        {
            var requestCreateBaseViewModel = new RequestCreateBaseViewModel()
            {
                Resources = await this.userService.GetAllActiveResourcesAndTheirSkills<UsersWithSkillsViewModel>(),
                Projects = await this.projectService.GetAllProjects<ProjectAllViewModel>(),
            };

            return this.View(requestCreateBaseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RequestCreateBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(bindingModel ?? new RequestCreateBindingModel());
            }

            await this.requestsService.Create(bindingModel);

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            var requests = await this.requestsService.GetAllRequests<RequestAllViewModel>();
            return this.View(requests);
        }
    }
}
