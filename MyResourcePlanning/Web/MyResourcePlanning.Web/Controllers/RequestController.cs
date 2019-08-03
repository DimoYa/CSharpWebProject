namespace MyResourcePlanning.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Common;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Services.Data.Request;
    using MyResourcePlanning.Web.BindingModels.Request;
    using MyResourcePlanning.Web.ViewModels.Request;

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

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> ResourceRequests()
        {
            var requests = await this.requestsService.GetAllResourceRequests<RequestAllViewModel>();
            return this.View(requests);
        }

        [Authorize(Roles = GlobalConstants.PlannerRoleName + "," +
                           GlobalConstants.ApproverRoleName)]
        public async Task<IActionResult> ShowComments(string id)
        {
            var comments = await this.requestsService.GetRequestCommentsById(id);
            return this.View(comments);
        }

        [Authorize(Roles = GlobalConstants.PlannerRoleName + "," +
                          GlobalConstants.ApproverRoleName)]
        public async Task<IActionResult> UserDetails()
        {
            return this.View(new RequestUserDetailsBaseModel());
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.PlannerRoleName + "," +
                           GlobalConstants.ApproverRoleName)]
        public async Task<IActionResult> UserDetails(RequestUserDetailsBaseModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model ?? new RequestUserDetailsBaseModel());
            }

            var userDetails = await this.requestsService.GetEmployeeDetails<RequestUserDetailsBaseModel>(model);

            return this.View(userDetails);
        }
    }
}
