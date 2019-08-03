namespace MyResourcePlanning.Services.Data.Request
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Web.BindingModels.Request;
    using MyResourcePlanning.Web.ViewModels.User;

    public interface IRequestService
    {
        Task<bool> Create(RequestCreateBindingModel requestCreateBindingModel);

        Task<bool> Edit(RequestEditBindingModel model, string id);

        Task<bool> Approve(string id, string comment);

        Task<bool> Reject(string id, string comment);

        Task<bool> Return(string id, string comment);

        Task<bool> Delete(string id);

        Task<RequestUserDetailsBaseModel> GetEmployeeDetails(RequestUserDetailsBaseModel model);

        Task<List<string>> GetRequestCommentsById(string id);

        Task<TViewModel> MapRequest<TViewModel>(string id);

        Task<IEnumerable<TViewModel>> GetAllPlannerRequests<TViewModel>();

        Task<IEnumerable<TViewModel>> GetAllApproverRequests<TViewModel>();

        Task<IEnumerable<TViewModel>> GetAllResourceRequests<TViewModel>();

    }
}
