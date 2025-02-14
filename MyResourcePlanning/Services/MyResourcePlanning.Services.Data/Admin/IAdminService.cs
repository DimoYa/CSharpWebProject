﻿namespace MyResourcePlanning.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Web.BindingModels.Admin;

    public interface IAdminService
    {
        Task<IEnumerable<TViewModel>> GetAllActiveUsers<TViewModel>();

        Task<bool> Lock(string id);

        Task<bool> Unlock(string id);

        Task ManageUserRoles(string id, AdminManageUserRolesBindingModel model);

        Task<bool> ManageUserApprover(string id, AdminManageApproverBindingModel model);

        Task<AdminManageUserRolesBindingModel> GetUserRolesById(string id);

        Task<AdminManageApproverBindingModel> GetUserApproverById(string id);
    }
}
