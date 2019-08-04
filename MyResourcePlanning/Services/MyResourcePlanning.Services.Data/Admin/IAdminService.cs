namespace MyResourcePlanning.Services.Data.Admin
{
    using MyResourcePlanning.Web.BindingModels.Admin;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<IEnumerable<TViewModel>> GetAllActiveUsers<TViewModel>();

        Task<bool> Lock(string id);

        Task<bool> Unlock(string id);

        Task ManageUserRoles(string id, AdminManageUserRolesBindingModel model);

        Task<AdminManageUserRolesBindingModel> GetUserRolesById(string id);
    }
}
