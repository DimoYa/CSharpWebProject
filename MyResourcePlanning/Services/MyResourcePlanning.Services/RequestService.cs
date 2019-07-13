namespace MyResourcePlanning.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Web.ViewModels;

    public class RequestService : IRequestService
    {
        private readonly MyResourcePlanningDbContext context;

        public RequestService(MyResourcePlanningDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<RequestAllViewModel> GetAllRequests()
        {
            var requests = this.context.Requests
                .Select(r => new RequestAllViewModel
                {
                    Project = r.Project.Name,
                    Resource = $"{r.User.FirstName} {r.User.LastName}",
                    Start = r.StartDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    End = r.StartDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Hours = r.WorkingHours.ToString(),
                    Status = r.Status.ToString(),
                }).ToList();

            return requests;
        }
    }
}
