namespace MyResourcePlanning.Services.Data.Request
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Web.BindingModels.Request;
    using MyResourcePlanning.Models;

    public interface IRequestService
    {
        Task<bool> Create(RequestCreateBindingModel requestCreateBindingModel);

        Task<bool> Edit(RequestEditBindingModel model, string id);

        Task<bool> Delete(string id);

        Task<Request> GetRequestById(string id);

        Task<TViewModel> MapRequest<TViewModel>(string id);

        Task<IEnumerable<TViewModel>> GetAllRequests<TViewModel>();
    }
}
