namespace MyResourcePlanning.Services.Data.Request
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Web.ViewModels.Request;

    public interface IRequestService
    {
        Task<IEnumerable<TViewModel>> GetAllRequests<TViewModel>();

        Task<bool> Create(RequestCreateBindingModel requestCreateBindingModel);
    }
}
