namespace MyResourcePlanning.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Web.ViewModels.User;

    [ViewComponent(Name ="ActiveResources")]
    public class ActiveResourcesViewComponent : ViewComponent
    {
        private readonly IUserService userService;

        public ActiveResourcesViewComponent(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var resources = await this.userService.GetAllActiveResources<UsersViewModel>();

            return this.View(resources);
        }
    }
}
