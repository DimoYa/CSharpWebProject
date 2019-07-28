namespace MyResourcePlanning.Services.Data.Request
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Models.Enums;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Request;

    public class RequestService : IRequestService
    {
        private readonly MyResourcePlanningDbContext context;
        private readonly IUserService userService;
        private readonly IProjectService projectService;

        public RequestService(
            MyResourcePlanningDbContext context,
            IUserService userService,
            IProjectService projectService)
        {
            this.context = context;
            this.userService = userService;
            this.projectService = projectService;
        }

        public async Task<bool> Create(RequestCreateBindingModel model)
        {
            var getResource = model.Resource.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            User resource = await this.GetResourceFromString(getResource);

            var getProject = model.Project.Split(new[] { '(' }, StringSplitOptions.RemoveEmptyEntries);
            Project project = await this.GetProjectFromString(getProject);

            Request request = new Request
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Project = project,
                User = resource,
                WorkingHours = model.WorkingHours,
                CreatedBy = await this.userService.GetCurrentUserId(),
            };

            this.context.Requests.Add(request);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Edit(RequestEditBindingModel model, string id)
        {
            var requestToUpdate = await this.GetRequestById(id);

            requestToUpdate.StartDate = model.StartDate;
            requestToUpdate.EndDate = model.EndDate;
            requestToUpdate.WorkingHours = model.WorkingHours;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var requestToDelete = await this.GetRequestById(id);

            requestToDelete.IsDeleted = true;
            requestToDelete.Status = RequestStatus.Deleted;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Approve(string id, string comment)
        {
            var requestToApprove = await this.GetRequestById(id);

            var commentToAdd = await this.AddCommentToRequest(requestToApprove, comment);

            requestToApprove.Status = RequestStatus.Booked;
            requestToApprove.Comment = commentToAdd;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Reject(string id, string comment)
        {
            var requestToReject = await this.GetRequestById(id);

            var commentToAdd = await this.AddCommentToRequest(requestToReject, comment);

            requestToReject.Status = RequestStatus.Rejected;
            requestToReject.Comment = commentToAdd;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Return(string id, string comment)
        {
            var requestToReturn = await this.GetRequestById(id);

            var commentToAdd = await this.AddCommentToRequest(requestToReturn, comment);

            requestToReturn.Status = RequestStatus.Returned;
            requestToReturn.Comment = commentToAdd;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<TViewModel> MapRequest<TViewModel>(string id)
        {
            var currentRequest = this.context.Requests
                 .Where(r => r.Id == id)
                 .To<TViewModel>()
                 .SingleOrDefault();

            return currentRequest;
        }

        public async Task<IEnumerable<TViewModel>> GetAllRequests<TViewModel>()
        {
            var requests = this.context.Requests
                .To<TViewModel>()
                .ToList();

            return requests;
        }

        public async Task<Request> GetRequestById(string id)
        {
            var currentRequest = this.context
               .Requests
               .SingleOrDefault(r => r.Id == id);

            return currentRequest;
        }

        private async Task<Project> GetProjectFromString(string[] getProject)
        {
            var projectName = getProject[0].Trim();
            var project = await this.projectService.GetProjectByName(projectName);
            return project;
        }

        private async Task<User> GetResourceFromString(string[] getResource)
        {
            var resourceFirstName = getResource[0].Trim();
            var resourceLastName = getResource[1].Trim();
            var resource = await this.userService.GetUserByName(resourceFirstName, resourceLastName);
            return resource;
        }

        private async Task<string> AddCommentToRequest(Request requestToApprove, string comment)
        {
            var sb = new StringBuilder();
            var commentHistory = requestToApprove.Comment;

            sb.AppendLine(commentHistory);

            var currentUser = await this.userService.GetCurrentUserEmail();

            var commentToAppend = $"{DateTime.UtcNow} {currentUser} {Environment.NewLine} {comment}";

            sb.AppendLine(commentToAppend);

            return sb.ToString();
        }
    }
}
