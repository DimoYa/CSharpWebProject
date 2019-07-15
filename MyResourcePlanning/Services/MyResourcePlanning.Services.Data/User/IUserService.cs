namespace MyResourcePlanning.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<TViewModel>> GetAllActiveResourcesAndTheirSkills<TViewModel>();

        Task<string> GetRoleIdByName(string roleName);
    }
}
