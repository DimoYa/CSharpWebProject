namespace MyResourcePlanning.Web.BindingModels.Request
{
    using System.Collections.Generic;

    using MyResourcePlanning.Web.ViewModels.Project;
    using MyResourcePlanning.Web.ViewModels.User;

    public class RequestCreateBaseBindingModel
    {
        public IEnumerable<ProjectAllViewModel> Projects { get; set; }

        public IEnumerable<UsersViewModel> Resources { get; set; }

        public RequestCreateBindingModel BindingModel { get; set; }
    }
}
