namespace MyResourcePlanning.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services;
    using MyResourcePlanning.Web.ViewModels.Request;
    using System;

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
            var resource = this.userService.GetAllActiveResourcesAndTheirSkills();
            var projects = this.projectService.GetAllProjects();

            var projectsAndResources = new RequestCreateResourcesAndProjects()
            {
                Resources = resource,
                Projects = projects,
            };

            return this.View(projectsAndResources);
        }

        [HttpPost]
        public IActionResult Create(RequestCreateBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Requests/Create");
            }

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            var requests = this.requestsService.GetAllRequests();
            return this.View(requests);
        }
    }
}
