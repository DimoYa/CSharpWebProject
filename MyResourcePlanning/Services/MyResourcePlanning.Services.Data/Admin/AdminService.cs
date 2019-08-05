namespace MyResourcePlanning.Services.Data.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MyResourcePlanning.Common;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Admin;

    public class AdminService : IAdminService
    {
        private readonly MyResourcePlanningDbContext context;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public AdminService(
            MyResourcePlanningDbContext context,
            IUserService userService,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<TViewModel>> GetAllActiveUsers<TViewModel>()
        {
            var activeUsers = this.context.Users
                .Include(u => u.Roles)
                .Where(u => u.IsDeleted == false)
                .OrderBy(u => u.Email);

            foreach (var user in activeUsers)
            {
                foreach (var role in user.Roles)
                {
                    var roleName = await this.userService.GetRoleNameById(role.RoleId);
                    role.RoleId = roleName;
                }
            }

            return activeUsers.To<TViewModel>().ToList();
        }

        public async Task<bool> Lock(string id)
        {
            var userToLock = await this.userService.GetUserById(id);

            await this.userManager.SetLockoutEndDateAsync(userToLock, DateTime.Now.AddHours(1));

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Unlock(string id)
        {
            var userToUnLock = await this.userService.GetUserById(id);

            await this.userManager.SetLockoutEndDateAsync(userToUnLock, null);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task ManageUserRoles(string id, AdminManageUserRolesBindingModel model)
        {
            var userToUpdate = await this.userService.GetUserById(id);

            var currentUserRoles = this.context.UserRoles
                .Where(u => u.UserId == id);

            this.context.UserRoles.RemoveRange(currentUserRoles);

            await this.context.SaveChangesAsync();

            if (model.Resource == true)
            {
                await this.userManager.AddToRoleAsync(userToUpdate, GlobalConstants.ResourceRoleName);
            }

            if (model.Admin == true)
            {
                await this.userManager.AddToRoleAsync(userToUpdate, GlobalConstants.AdministratorRoleName);
            }

            if (model.Planner == true)
            {
                await this.userManager.AddToRoleAsync(userToUpdate, GlobalConstants.PlannerRoleName);
            }

            if (model.Approver == true)
            {
                await this.userManager.AddToRoleAsync(userToUpdate, GlobalConstants.ApproverRoleName);
            }
        }

        public async Task<AdminManageUserRolesBindingModel> GetUserRolesById(string userId)
        {
            var user = this.context.Users
               .Include(u => u.Roles)
               .Where(u => u.IsDeleted == false)
               .Where(u => u.Id == userId)
               .SingleOrDefault();

            var model = new AdminManageUserRolesBindingModel();

            model.FullName = $"{user.FirstName} {user.LastName}";

            foreach (var role in user.Roles)
            {
                var roleName = await this.userService.GetRoleNameById(role.RoleId);

                switch (roleName)
                {
                    case GlobalConstants.ResourceRoleName:
                        model.Resource = true;
                        break;
                    case GlobalConstants.AdministratorRoleName:
                        model.Admin = true;
                        break;
                    case GlobalConstants.PlannerRoleName:
                        model.Planner = true;
                        break;
                    case GlobalConstants.ApproverRoleName:
                        model.Approver = true;
                        break;
                }
            }

            return model;
        }

        public async Task<AdminManageApproverBindingModel> GetUserApproverById(string id)
        {
            var currentUser = await this.userService.GetUserById(id);

            var approverFirstName = currentUser.Approver == null
                ? string.Empty
                : currentUser.Approver.FirstName;

            var approverLastName = currentUser.Approver == null
                ? string.Empty
                : currentUser.Approver.LastName;

            var model = new AdminManageApproverBindingModel()
            {
                CurrentApprover = $"{approverFirstName} {approverLastName}",
            };

            return model;
        }

        public async Task<bool> ManageUserApprover(string id, AdminManageApproverBindingModel model)
        {
            var fullMame = Regex.Split(model.FullName, @"\s\s+");

            var firstName = fullMame[0];
            var lastName = fullMame[1];

            var approver = await this.userService.GetUserByName(firstName, lastName);

            var currentUserApprover = await this.userService.GetUserById(id);

            currentUserApprover.Approver = approver;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
