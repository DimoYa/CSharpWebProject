namespace MyResourcePlanning.Services.Data.Project
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Services.Mapping;

    public class ProjectService : IProjectService
    {
        private readonly MyResourcePlanningDbContext context;

        public ProjectService(MyResourcePlanningDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TViewModel>> GetAllProjects<TViewModel>()
        {
            var projects = this.context.Projects
                .To<TViewModel>()
                .ToList();

            return projects;
        }
    }
}
