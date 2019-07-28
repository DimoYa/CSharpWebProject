namespace MyResourcePlanning.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Models;

    public interface IUserService
    {
        Task<IEnumerable<TViewModel>> GetAllActiveResources<TViewModel>();

        Task<string> GetRoleIdByName(string roleName);

        Task<string> GetCurrentUserId();

        Task<string> GetCurrentUserEmail();

        Task<User> GetUserByName(string firstName, string lastName);
    }
}
