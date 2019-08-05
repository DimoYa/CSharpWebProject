namespace MyResourcePlanning.Tests.Common
{
    using Microsoft.EntityFrameworkCore;
    using MyResourcePlanning.Data;
    using System;

    public static class MyResourcePlanningDbContextInMemoryFactory
    {
        public static MyResourcePlanningDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<MyResourcePlanningDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new MyResourcePlanningDbContext(options);
        }
    }
}
