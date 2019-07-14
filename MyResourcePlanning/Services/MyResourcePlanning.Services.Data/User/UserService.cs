namespace MyResourcePlanning.Services.Data.User
{
    using System.Collections.Generic;
    using System.Linq;

    using MyResourcePlanning.Common;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Services.Mapping;

    public class UserService : IUserService
    {
        private readonly MyResourcePlanningDbContext context;

        public UserService(MyResourcePlanningDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TViewModel> GetAllActiveResourcesAndTheirSkills<TViewModel>()
        {
            var resourceRoleId = this.GetRoleIdByName(GlobalConstants.ResourceRoleName);

            var userWithSkills = this.context.Users
                .Where(u => u.IsDeleted == false && u.Roles.Any(r => r.RoleId == resourceRoleId))
                .To<TViewModel>()
                .ToList();

            return userWithSkills;
        }

        public string GetRoleIdByName(string roleName)
        {
            var resourceRoleId = this.context.Roles
                .FirstOrDefault(x => x.Name == roleName)
                .Id;

            return resourceRoleId;
        }
    }
}
