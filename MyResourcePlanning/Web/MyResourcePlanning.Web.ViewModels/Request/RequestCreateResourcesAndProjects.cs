namespace MyResourcePlanning.Web.ViewModels.Request
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyResourcePlanning.Web.ViewModels.Project;
    using MyResourcePlanning.Web.ViewModels.User;

    public class RequestCreateResourcesAndProjects
    {
        public IEnumerable<ProjectAllViewModel> Projects { get; set; }

        public IEnumerable<UsersWithSkillsViewModel> Resources { get; set; }

        public RequestCreateBindingModel BindingModel { get; set; }
    }
}
