namespace MyResourcePlanning.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MyResourcePlanning.Common;
    using MyResourcePlanning.Models;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(MyResourcePlanningDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.PlannerRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.ApproverRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<UserRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new UserRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
