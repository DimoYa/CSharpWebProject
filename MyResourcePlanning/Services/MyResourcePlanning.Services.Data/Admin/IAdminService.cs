namespace MyResourcePlanning.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<IEnumerable<TViewModel>> GetAllActiveUsers<TViewModel>();

        Task<bool> Lock(string id);

        Task<bool> Unlock(string id);
    }
}
