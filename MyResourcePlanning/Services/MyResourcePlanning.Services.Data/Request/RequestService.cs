namespace MyResourcePlanning.Services.Data.Request
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.ViewModels.Request;

    public class RequestService : IRequestService
    {
        private readonly MyResourcePlanningDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RequestService(
            MyResourcePlanningDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
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
                CreatedBy = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
            };

            this.context.Requests.Add(request);
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
}
}
