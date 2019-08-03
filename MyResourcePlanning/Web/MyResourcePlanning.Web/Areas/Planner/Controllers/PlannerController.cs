namespace MyResourcePlanning.Web.Areas.Planner.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Common;
    using MyResourcePlanning.Web.Controllers;

    [Authorize(Roles = GlobalConstants.PlannerRoleName)]
    [Area("Planner")]
    public abstract class PlannerController : BaseController
    {
    }
}
