namespace MyResourcePlanning.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Admin;
    using MyResourcePlanning.Web.BindingModels.Admin;
    using MyResourcePlanning.Web.ViewModels.Admin;

    public class UserManagementController : AdminController
    {
        private readonly IAdminService adminService;

        public UserManagementController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<IActionResult> Lock(string id)
        {
            await this.adminService.Lock(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Unlock(string id)
        {
            await this.adminService.Unlock(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> ManageUserRoles(string id)
        {
            var categoryForUpdate = await this.adminService.GetUserRolesById(id);

            return this.View(categoryForUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(AdminManageUserRolesBindingModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model ?? new AdminManageUserRolesBindingModel());
            }

            await this.adminService.ManageUserRoles(id, model);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var activeUsers = await this.adminService.GetAllActiveUsers<AdminAllUsersViewModel>();
            return this.View(activeUsers);
        }
    }
}
