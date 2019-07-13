namespace MyResourcePlanning.Services
{
    using System.Collections.Generic;

    using MyResourcePlanning.Web.ViewModels.User;

    public interface IUserService
    {
        IEnumerable<UsersWithSkillsViewModel> GetAllActiveResourcesAndTheirSkills();
    }
}
