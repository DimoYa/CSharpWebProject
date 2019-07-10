using System.Collections.Generic;
using System.Linq;

using MyResourcePlanning.Data;
using MyResourcePlanning.Web.ViewModels.User;

namespace MyResourcePlanning.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<UsersWithSkillsViewModel> GetAllActiveResourcesAndTheirSkills()
        {
            var userWithSkills = this.context.Users
                .Where(u => u.IsDeleted == false)
                .Select(u => new UsersWithSkillsViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Skills = u.Skills.Select(s => s.Skill.Name)
                     .ToList(),
                }).ToList();

            return userWithSkills;
        }
    }
}
