namespace MyResourcePlanning.Services.Data.User
{
    using System.Collections.Generic;

    public interface IUserService
    {
        IEnumerable<TViewModel> GetAllActiveResourcesAndTheirSkills<TViewModel>();
    }
}
