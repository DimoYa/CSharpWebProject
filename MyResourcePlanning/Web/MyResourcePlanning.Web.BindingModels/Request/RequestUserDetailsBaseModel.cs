using MyResourcePlanning.Web.ViewModels.User;

namespace MyResourcePlanning.Web.BindingModels.Request
{
    public class RequestUserDetailsBaseModel
    {
        public RequestCreateUserDetailsBindingModel BindingnModel { get; set; }
        public UserDetails ViewModel { get; set; }
    }
}
