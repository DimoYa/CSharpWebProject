namespace MyResourcePlanning.Services.Data.Request
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Models.Enums;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Request;

    public class RequestService : IRequestService
    {
        private readonly MyResourcePlanningDbContext context;
        private readonly IUserService userService;

        public RequestService(
            MyResourcePlanningDbContext context,
            IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<bool> Create(RequestCreateBindingModel model)
        {
            Request request = new Request
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ProjectId = model.Project.Split(';')[0],
                UserId = model.Resource.Split(';')[0],
                WorkingHours = model.WorkingHours,
                CreatedBy = await this.userService.GetCurrentUserId(),
            };

            this.context.Requests.Add(request);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteById(string id)
        {
            var requestToDelete = await this.GetRequestById(id);

            requestToDelete.IsDeleted = true;
            requestToDelete.Status = RequestStatus.Deleted;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<TViewModel>> GetAllRequests<TViewModel>()
        {
            var requests = this.context.Requests
                .To<TViewModel>()
                .ToList();

            return requests;
        }

        private async Task<Request> GetRequestById(string id)
        {
            var currentRequest = this.context
               .Requests
               .SingleOrDefault(p => p.Id == id);

            return currentRequest;
        }
    }
}
