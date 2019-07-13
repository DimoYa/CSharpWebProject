namespace MyResourcePlanning.Services.Data.Request
{
    using System.Collections.Generic;

    public interface IRequestService
    {
        IEnumerable<TViewModel> GetAllRequests<TViewModel>();
    }
}
