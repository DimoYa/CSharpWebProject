namespace MyResourcePlanning.Web.ViewModels.Request
{
    using System.Collections.Generic;

    using MyResourcePlanning.Web.ViewModels.Project;
    using MyResourcePlanning.Web.ViewModels.User;

    public class RequestCreateBaseViewModel
    {
        public IEnumerable<ProjectAllViewModel> Projects { get; set; }

        public IEnumerable<UsersWithSkillsViewModel> Resources { get; set; }

        public RequestCreateBindingModel BindingModel { get; set; }
    }
}
