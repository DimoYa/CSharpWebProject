namespace MyResourcePlanning.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Services.Data.Request;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Web.BindingModels.Request;
    using MyResourcePlanning.Web.ViewModels.Request;
    using MyResourcePlanning.Web.ViewModels.User;

    public class RequestController : BaseController
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

        public async Task<IActionResult> Approve(string id, string comment)
        {
            await this.requestsService.Approve(id, comment);

            return this.RedirectToAction(nameof(this.ApproverRequests));
        }

        public async Task<IActionResult> Reject(string id, string comment)
        {
            await this.requestsService.Reject(id, comment);

            return this.RedirectToAction(nameof(this.ApproverRequests));
        }

        public async Task<IActionResult> Return(string id, string comment)
        {
            await this.requestsService.Return(id, comment);

            return this.RedirectToAction(nameof(this.ApproverRequests));
        }

        public async Task<IActionResult> PlannnerRequests()
        {
            var requests = await this.requestsService.GetAllPlannerRequests<RequestAllViewModel>();
            return this.View(requests);
        }

        public async Task<IActionResult> ApproverRequests()
        {
            var requests = await this.requestsService.GetAllApproverRequests<RequestAllViewModel>();
            return this.View(requests);
        }

        public async Task<IActionResult> ResourceRequests()
        {
            var requests = await this.requestsService.GetAllResourceRequests<RequestAllViewModel>();
            return this.View(requests);
        }

        public async Task<IActionResult> ShowComments(string id)
        {
            var comments = await this.requestsService.GetRequestCommentsById(id);
            return this.View(comments);
        }

        public async Task<IActionResult> UserDetails()
        {
            return this.View(new RequestUserDetailsBaseModel());
        }

        [HttpPost]
        public async Task<IActionResult> UserDetails(RequestUserDetailsBaseModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model ?? new RequestUserDetailsBaseModel());
            }

            var userDetails = await this.requestsService.GetEmployeeDetails(model);

            return this.View(userDetails);
        }
    }
}
