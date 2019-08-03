namespace MyResourcePlanning.Services.Data.User
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Models;
    using MyResourcePlanning.Web.ViewModels.User;

    public interface IUserService
    {
        Task<IEnumerable<TViewModel>> GetAllActiveResources<TViewModel>();

        Task<string> GetRoleIdByName(string roleName);

        Task<string> GetCurrentUserId();

        Task<string> GetCurrentUserName();

        Task<User> GetUserByName(string firstName, string lastName);
    }
}
