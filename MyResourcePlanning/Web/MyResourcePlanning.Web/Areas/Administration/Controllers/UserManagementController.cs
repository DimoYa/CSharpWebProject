using Microsoft.AspNetCore.Mvc;
using MyResourcePlanning.Services.Data.Admin;
using MyResourcePlanning.Web.ViewModels.Admin;
using System.Threading.Tasks;

namespace MyResourcePlanning.Web.Areas.Administration.Controllers
{
    public class UserManagementController : AdminController
    {
        private readonly IAdminService adminService;

        public UserManagementController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<IActionResult> All()
        {
            var activeUsers = await this.adminService.GetAllActiveUsers<AdminAllUsersViewModel>();
            return this.View(activeUsers);
        }
    }
}
