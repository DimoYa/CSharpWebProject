namespace MyResourcePlanning.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Services.Mapping;


    public class AdminService : IAdminService
    {
        private readonly MyResourcePlanningDbContext context;
        private readonly IUserService userService;

        public AdminService(MyResourcePlanningDbContext context,
            IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<IEnumerable<TViewModel>> GetAllActiveUsers<TViewModel>()
        {
            var activeUsers = this.context.Users
                .Include(u => u.Roles)
                .Where(u => u.IsDeleted == false);

            foreach (var user in activeUsers)
            {
                foreach (var role in user.Roles)
                {
                    var roleName = await this.userService.GetRoleNameById(role.RoleId);
                    role.RoleId = roleName;
                }
            }

            return activeUsers.To<TViewModel>().ToList();
        }
    }
}
