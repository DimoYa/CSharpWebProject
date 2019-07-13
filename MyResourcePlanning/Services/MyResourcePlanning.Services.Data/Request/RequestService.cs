namespace MyResourcePlanning.Services.Data.Request
{
    using System.Collections.Generic;
    using System.Linq;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Services.Mapping;

    public class RequestService : IRequestService
    {
        private readonly MyResourcePlanningDbContext context;

        public RequestService(MyResourcePlanningDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TViewModel> GetAllRequests<TViewModel>()
        {
            var requests = this.context.Requests
                .To<TViewModel>()
                .ToList();

            return requests;
        }
    }
}
