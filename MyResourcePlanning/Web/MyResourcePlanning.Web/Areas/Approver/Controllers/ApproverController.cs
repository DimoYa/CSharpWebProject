namespace MyResourcePlanning.Web.Areas.Approver.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Common;
    using MyResourcePlanning.Web.Controllers;

    [Authorize(Roles = GlobalConstants.ApproverRoleName)]
    [Area("Approver")]
    public abstract class ApproverController : BaseController
    {
    }
}
