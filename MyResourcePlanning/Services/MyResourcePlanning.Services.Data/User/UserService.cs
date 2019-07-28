namespace MyResourcePlanning.Services.Data.User
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyResourcePlanning.Common;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;

    public class UserService : IUserService
    {
        private readonly MyResourcePlanningDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(
            MyResourcePlanningDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<TViewModel>> GetAllActiveResources<TViewModel>()
        {
            var resourceRoleId = await this.GetRoleIdByName(GlobalConstants.ResourceRoleName);

            var userWithSkills = this.context.Users
                .Where(u => u.IsDeleted == false && u.Roles.Any(r => r.RoleId == resourceRoleId))
                .To<TViewModel>()
                .ToList();

            return userWithSkills;
        }

        public async Task<string> GetCurrentUserId()
        {
            var currentUser = this.httpContextAccessor
                .HttpContext
                .User
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;

            return currentUser;
        }

        public async Task<string> GetCurrentUserName()
        {
            var currentUser = this.httpContextAccessor
                .HttpContext
                .User
                .FindFirst(ClaimTypes.Name)
                .Value;

            return currentUser;
        }

        public async Task<User> GetUserByName(string firstName, string lastName)
        {
            var user = this.context.Users
                .SingleOrDefault(u => u.FirstName == firstName && u.LastName == lastName);

            return user;
        }

        public async Task<string> GetRoleIdByName(string roleName)
        {
            var resourceRoleId = this.context.Roles
                .FirstOrDefault(x => x.Name == roleName)
                .Id;

            return resourceRoleId;
        }
    }
}
