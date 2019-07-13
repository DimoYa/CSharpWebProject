namespace MyResourcePlanning.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(MyResourcePlanningDbContext dbContext, IServiceProvider serviceProvider);
    }
}
