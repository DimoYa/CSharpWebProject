namespace MyResourcePlanning.Services.Data.Project
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.ViewModels.Project;

    public class ProjectService : IProjectService
    {
        private readonly MyResourcePlanningDbContext context;

        public ProjectService(MyResourcePlanningDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(ProjectCreateInputModel model)
        {
            Project project = new Project
            {
                Name = model.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                RequestedHours = model.RequestedHours,
            };

            this.context.Projects.Add(project);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteById(string id)
        {
            var projectToDelete = await this.GetProjectById(id);

            projectToDelete.IsDeleted = true;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<TViewModel>> GetAllProjects<TViewModel>()
        {
            var projects = this.context.Projects
                .To<TViewModel>()
                .ToList();

            return projects;
        }

        private async Task<Project> GetProjectById(string id)
        {
            var currentProject = this.context
                .Projects
                .SingleOrDefault(p => p.Id == id);

            return currentProject;
        }
    }
}
