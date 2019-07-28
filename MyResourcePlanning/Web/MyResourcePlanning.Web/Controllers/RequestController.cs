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

            return this.RedirectToAction(nameof(this.PlannnerRequests));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var requestForUpdate = await this.requestsService.MapRequest<RequestAllViewModel>(id);

            return this.View(requestForUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RequestEditBindingModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(new RequestAllViewModel());
            }

            await this.requestsService.Edit(model, id);

            return this.RedirectToAction(nameof(this.PlannnerRequests));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.requestsService.Delete(id);

            return this.RedirectToAction(nameof(this.PlannnerRequests));
        }

        public async Task<IActionResult> Approve(string id, string comment)
        {
            await this.requestsService.Approve(id, comment);

            return this.RedirectToAction(nameof(this.PlannnerRequests));
        }

        public async Task<IActionResult> Reject(string id, string comment)
        {
            await this.requestsService.Reject(id, comment);

            return this.RedirectToAction(nameof(this.PlannnerRequests));
        }

        public async Task<IActionResult> Return(string id, string comment)
        {
            await this.requestsService.Return(id, comment);

            return this.RedirectToAction(nameof(this.PlannnerRequests));
        }

        public async Task<IActionResult> PlannnerRequests()
        {
            var requests = await this.requestsService.GetAllRequests<RequestAllViewModel>();
            return this.View(requests);
        }

        private async Task<RequestCreateBaseBindingModel> GetRequestBaseModel()
        {
            return new RequestCreateBaseBindingModel()
            {
                Resources = await this.userService.GetAllActiveResources<UsersViewModel>(),
                Projects = await this.projectService.GetAllProjects<ProjectAllViewModel>(),
            };
        }
    }
}
