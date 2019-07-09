namespace MyResourcePlanning.Services
{
    using MyResourcePlanning.Web.ViewModels;
    using System.Collections.Generic;

    public interface IRequestService
    {
        List<RequestAllViewModel> GetAllRequests();
    }
}
