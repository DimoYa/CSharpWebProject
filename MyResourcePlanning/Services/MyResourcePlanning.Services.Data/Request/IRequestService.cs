namespace MyResourcePlanning.Services.Data.Request
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Models;
    using MyResourcePlanning.Web.BindingModels.Request;

    public interface IRequestService
    {
        Task<bool> Create(RequestCreateBindingModel requestCreateBindingModel);

        Task<bool> Edit(RequestEditBindingModel model, string id);

        Task<bool> Approve(string id, string comment);

        Task<bool> Reject(string id, string comment);

        Task<bool> Return(string id, string comment);

        Task<bool> Delete(string id);

        Task<List<string>> GetRequestCommentsById(string id);

        Task<TViewModel> MapRequest<TViewModel>(string id);

        Task<IEnumerable<TViewModel>> GetAllPlannerRequests<TViewModel>();

        Task<IEnumerable<TViewModel>> GetAllApproverRequests<TViewModel>();

        Task<IEnumerable<TViewModel>> GetAllResourceRequests<TViewModel>();
    }
}
