namespace MyResourcePlanning.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Web.ViewModels.Admin;

    [ViewComponent(Name = "ActiveApprovers")]
    public class ActiveApproversViewComponent : ViewComponent
    {
        private readonly IUserService userService;

        public ActiveApproversViewComponent(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var approvers = await this.userService.GetAllActiveApprovers<AdminUserApproversViewModel>();

            return this.View(approvers);
        }
    }
}
