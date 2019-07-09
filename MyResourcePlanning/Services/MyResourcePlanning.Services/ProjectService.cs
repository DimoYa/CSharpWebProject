namespace MyResourcePlanning.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Web.ViewModels.Project;

    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext context;

        public ProjectService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<ProjectAllViewModel> GetAllProjects()
        {
            var projects = this.context.Projects
                .Select(p => new ProjectAllViewModel
                {
                    Name = p.Name,
                    Start = p.StartDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    End = p.StartDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                }).ToList();

            return projects;
        }
    }
}
