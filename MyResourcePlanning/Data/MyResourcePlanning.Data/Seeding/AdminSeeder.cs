namespace MyResourcePlanning.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MyResourcePlanning.Common;
    using MyResourcePlanning.Models;

    internal class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(MyResourcePlanningDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();

            var adminUser = new User()
            {
                UserName = "admin@admin.bg",
                Email = "admin@admin.bg",
                FirstName = "Admin",
                LastName = "Admin",
            };

            await SeedAdminAsync(userManager, roleManager, adminUser);
        }

        private static async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<UserRole> roleManager, User admin)
        {
            var user = await userManager.FindByEmailAsync(admin.Email);
            if (user == null)
            {
                var result = await userManager.CreateAsync(admin, "admin");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }

            var adminRoleName = GlobalConstants.AdministratorRoleName;

            var role = await roleManager.FindByNameAsync(adminRoleName);

            if (role != null)
            {
                var adminUser = await userManager.FindByEmailAsync(admin.Email);
                var result = await userManager.AddToRoleAsync(adminUser, adminRoleName);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
