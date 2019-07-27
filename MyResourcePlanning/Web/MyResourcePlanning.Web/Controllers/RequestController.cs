namespace MyResourcePlanning.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Services.Data.Request;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Web.BindingModels.Request;
    using MyResourcePlanning.Web.ViewModels.Project;
    using MyResourcePlanning.Web.ViewModels.Request;
    using MyResourcePlanning.Web.ViewModels.User;

    public class RequestController : BaseController
    {
        private readonly IRequestService requestsService;
        private readonly IUserService userService;
        private readonly IProjectService projectService;

        public RequestController(
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
            var requestCreateBaseViewModel = await this.GetRequestBaseModel();

            return this.View(requestCreateBaseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RequestCreateBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                var requestCreateBaseViewModel = await this.GetRequestBaseModel();

                return this.View(requestCreateBaseViewModel);
            }

            await this.requestsService.Create(bindingModel);

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            var requests = await this.requestsService.GetAllRequests<RequestAllViewModel>();
            return this.View(requests);
        }

        private async Task<RequestCreateBaseViewModel> GetRequestBaseModel()
        {
            return new RequestCreateBaseViewModel()
            {
                Resources = await this.userService.GetAllActiveResources<UsersViewModel>(),
                Projects = await this.projectService.GetAllProjects<ProjectAllViewModel>(),
            };
        }
    }
}
