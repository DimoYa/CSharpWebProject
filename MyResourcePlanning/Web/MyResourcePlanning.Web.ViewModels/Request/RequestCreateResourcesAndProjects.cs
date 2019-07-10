using MyResourcePlanning.Web.ViewModels.Project;
using MyResourcePlanning.Web.ViewModels.User;

using System;
using System.Collections.Generic;
using System.Text;

namespace MyResourcePlanning.Web.ViewModels.Request
{
    public class RequestCreateResourcesAndProjects
    {
        public List<ProjectAllViewModel> Projects { get; set; }

        public List<UsersWithSkillsViewModel> Resources { get; set; }
    }
}
