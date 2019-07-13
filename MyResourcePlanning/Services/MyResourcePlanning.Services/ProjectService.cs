namespace MyResourcePlanning.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Services.Mapping;

    public class ProjectService : IProjectService
    {
        private readonly MyResourcePlanningDbContext context;

        public ProjectService(MyResourcePlanningDbContext context)
        {
            this.context = context;
        }

        IEnumerable<TViewModel> IProjectService.GetAllProjects<TViewModel>()
        {
            var projects = this.context.Projects
                .To<TViewModel>()
                .ToList();

            return projects;
        }
    }
}
