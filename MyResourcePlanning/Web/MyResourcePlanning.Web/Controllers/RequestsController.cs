namespace MyResourcePlanning.Web.Controllers
{
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

        public IActionResult Create()
        {
            var requestCreateBaseViewModel = new RequestCreateBaseViewModel()
            {
                Resources = this.userService.GetAllActiveResourcesAndTheirSkills<UsersWithSkillsViewModel>(),
                Projects = this.projectService.GetAllProjects<ProjectAllViewModel>(),
            };

            return this.View(requestCreateBaseViewModel);
        }

        [HttpPost]
        public IActionResult Create(RequestCreateBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(bindingModel ?? new RequestCreateBindingModel());
            }

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            var requests = this.requestsService.GetAllRequests<RequestAllViewModel>();
            return this.View(requests);
        }
    }
}
