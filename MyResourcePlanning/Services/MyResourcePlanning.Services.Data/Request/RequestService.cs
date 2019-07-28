namespace MyResourcePlanning.Services.Data.Request
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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
            var getResource = Regex.Split(model.Resource, @"\s\s+");
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
            requestToUpdate.Status = RequestStatus.InProgress;

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

            if (!string.IsNullOrEmpty(comment))
            {
                var commentToAdd = await this.AddCommentToRequest(requestToApprove, comment);
                requestToApprove.Comment = commentToAdd;
            }

            requestToApprove.Status = RequestStatus.Booked;
            requestToApprove.Project.RequestedHours -= requestToApprove.WorkingHours;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Reject(string id, string comment)
        {
            var requestToReject = await this.GetRequestById(id);

            if (!string.IsNullOrEmpty(comment))
            {
                var commentToAdd = await this.AddCommentToRequest(requestToReject, comment);
                requestToReject.Comment = commentToAdd;
            }

            requestToReject.Status = RequestStatus.Rejected;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Return(string id, string comment)
        {
            var requestToReturn = await this.GetRequestById(id);

            if (!string.IsNullOrEmpty(comment))
            {
                var commentToAdd = await this.AddCommentToRequest(requestToReturn, comment);
                requestToReturn.Comment = commentToAdd;
            }

            requestToReturn.Status = RequestStatus.Returned;

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

        public async Task<IEnumerable<TViewModel>> GetAllPlannerRequests<TViewModel>()
        {
            var currentUser = await this.userService.GetCurrentUserId();

            var requests = this.context.Requests
                .Where(r => r.CreatedBy == currentUser)
                .To<TViewModel>()
                .ToList();

            return requests;
        }

        public async Task<IEnumerable<TViewModel>> GetAllApproverRequests<TViewModel>()
        {
            var currentUser = await this.userService.GetCurrentUserId();

            var requests = this.context.Requests
                .Where(r => r.User.ApproverId == currentUser)
                .To<TViewModel>()
                .ToList();

            return requests;
        }

        public async Task<IEnumerable<TViewModel>> GetAllResourceRequests<TViewModel>()
        {
            var currentUser = await this.userService.GetCurrentUserId();

            var requests = this.context.Requests
                .Where(r => r.UserId == currentUser)
                .Where(r => r.IsDeleted == false)
                .To<TViewModel>()
                .ToList();

            return requests;
        }

        public async Task<List<string>> GetRequestCommentsById(string id)
        {
            var currentRequest = await this.GetRequestById(id);

            var comments = currentRequest
                .Comment;

            if (comments == null)
            {
                return new List<string>();
            }

            return comments
                .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }

        private async Task<Request> GetRequestById(string id)
        {
            var currentRequest = this.context
               .Requests
               .Include(p => p.Project)
               .Include(u => u.User)
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

            sb.Append(commentHistory);

            var currentUser = await this.userService.GetCurrentUserName();

            var commentToAppend = $"{DateTime.UtcNow} {currentUser} {Environment.NewLine} {comment} -";

            sb.AppendLine(commentToAppend);

            return sb.ToString().Trim();
        }
    }
}
