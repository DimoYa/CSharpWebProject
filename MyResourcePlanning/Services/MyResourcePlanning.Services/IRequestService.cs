namespace MyResourcePlanning.Services
{
    using System.Collections.Generic;

    using MyResourcePlanning.Web.ViewModels;

    public interface IRequestService
    {
        IEnumerable<RequestAllViewModel> GetAllRequests();
    }
}
