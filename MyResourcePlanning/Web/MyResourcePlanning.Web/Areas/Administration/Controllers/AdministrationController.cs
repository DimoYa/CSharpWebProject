namespace MyResourcePlanning.Web.Areas.Administration.Controllers
{
    using MyResourcePlanning.Common;
    using MyResourcePlanning.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
