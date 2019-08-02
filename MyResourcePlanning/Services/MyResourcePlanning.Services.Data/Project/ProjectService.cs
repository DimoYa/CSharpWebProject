namespace MyResourcePlanning.Services.Data.Project
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Project;

    public class ProjectService : IProjectService
    {
        private readonly MyResourcePlanningDbContext context;

        public ProjectService(MyResourcePlanningDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(ProjectCreateBindingModel model)
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

        public async Task<bool> Delete(string id)
        {
            var projectToDelete = await this.GetProjectById(id);

            projectToDelete.IsDeleted = true;
            projectToDelete.DeletedOn = DateTime.UtcNow;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Edit(ProjectEditBindingModel model, string id)
        {
            var projectToUpdate = await this.GetProjectById(id);

            projectToUpdate.Name = model.Name;
            projectToUpdate.StartDate = model.StartDate;
            projectToUpdate.EndDate = model.EndDate;
            projectToUpdate.RequestedHours = model.RequestedHours;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public Task<IEnumerable<TViewModel>> GetAllProjects<TViewModel>()
        {
            var projects = this.context.Projects
                .Where(p => p.IsDeleted == false)
                .To<TViewModel>()
                .ToList();

            return Task.FromResult(projects.AsEnumerable());
        }

        public Task<IEnumerable<TViewModel>> GetAllProjectsForRequest<TViewModel>()
        {
            var projects = this.context.Projects
                .Where(p => p.IsDeleted == false
                && p.RequestedHours > 0
                && p.EndDate >= DateTime.Now)
                .To<TViewModel>()
                .ToList();

            return Task.FromResult(projects.AsEnumerable());
        }

        public Task<TViewModel> MapProject<TViewModel>(string id)
        {
            var currentProject = this.context.Projects
                 .Where(p => p.Id == id)
                 .To<TViewModel>()
                 .SingleOrDefault();

            return Task.FromResult(currentProject);
        }

        public Task<Project> GetProjectById(string id)
        {
            var currentProject = this.context
                .Projects
                .Where(p => p.IsDeleted == false)
                .SingleOrDefault(p => p.Id == id);

            return Task.FromResult(currentProject);
        }

        public Task<Project> GetProjectByName(string name)
        {
            var currentProject = this.context
                .Projects
                .Where(p => p.IsDeleted == false)
                .FirstOrDefault(p => p.Name == name);

            return Task.FromResult(currentProject);
        }
    }
}
