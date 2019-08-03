namespace MyResourcePlanning.Web.Areas.Approver.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Services.Data.Request;
    using MyResourcePlanning.Web.ViewModels.Request;

    public class RequestController : ApproverController
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

        public async Task<IActionResult> ApproverRequests()
        {
            var requests = await this.requestsService.GetAllApproverRequests<RequestAllViewModel>();
            return this.View(requests);
        }
    }
}
