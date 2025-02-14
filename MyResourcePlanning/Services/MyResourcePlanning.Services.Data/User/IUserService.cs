﻿namespace MyResourcePlanning.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Models;

    public interface IUserService
    {
        Task<IEnumerable<TViewModel>> GetAllActiveResources<TViewModel>();

        Task<IEnumerable<TViewModel>> GetAllActiveApprovers<TViewModel>();

        Task<string> GetRoleIdByName(string roleName);

        Task<string> GetRoleNameById(string roleId);

        Task<string> GetCurrentUserId();

        Task<string> GetCurrentUserName();

        Task<User> GetUserByName(string firstName, string lastName);

        Task<User> GetUserById(string id);
    }
}
