namespace MyResourcePlanning.Web.Areas.Planner.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Services.Data.Request;
    using MyResourcePlanning.Web.BindingModels.Request;
    using MyResourcePlanning.Web.ViewModels.Request;

    public class RequestController : PlannerController
    {
        private readonly IRequestService requestsService;
        private readonly IProjectService projectService;

        public RequestController(
            IRequestService requestsService,
            IProjectService projectService)
        {
            this.requestsService = requestsService;
            this.projectService = projectService;
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RequestCreateBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(bindingModel ?? new RequestCreateBindingModel());
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
                return this.View(new RequestEditBindingModel());
            }

            await this.requestsService.Edit(model, id);

            return this.RedirectToAction(nameof(this.PlannnerRequests));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.requestsService.Delete(id);

            return this.RedirectToAction(nameof(this.PlannnerRequests));
        }

        public async Task<IActionResult> PlannnerRequests()
        {
            var requests = await this.requestsService.GetAllPlannerRequests<RequestAllViewModel>();
            return this.View(requests);
        }
    }
}
