namespace MyResourcePlanning.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;


    public class AdminService : IAdminService
    {
        private readonly MyResourcePlanningDbContext context;
        private readonly UserManager<User> userManager;

        public AdminService(MyResourcePlanningDbContext context,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public Task<IEnumerable<TViewModel>> GetAllActiveUsers<TViewModel>()
        {
            var activeUsers = this.context.Users
                .Include(u => u.Roles)
                .Where(u => u.IsDeleted == false)
                .To<TViewModel>()
                .ToList();

            return Task.FromResult(activeUsers.AsEnumerable());
        }
    }
}
