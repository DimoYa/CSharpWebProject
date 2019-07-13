namespace MyResourcePlanning.Services.Data.User
{
    using System.Collections.Generic;
    using System.Linq;

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
            var userWithSkills = this.context.Users
                .Where(u => u.IsDeleted == false)
                .To<TViewModel>()
                .ToList();

            return userWithSkills;
        }
    }
}
