using MyResourcePlanning.Web.ViewModels.User;

using System.Collections.Generic;

namespace MyResourcePlanning.Services
{
    public interface IUserService
    {
        List<UsersWithSkillsViewModel> GetAllActiveResourcesAndTheirSkills();
    }
}
