namespace MyResourcePlanning.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services;

    public class RequestsController : BaseController
    {
        private readonly IRequestService requestsService;

        public RequestsController(IRequestService requestsService)
        {
            this.requestsService = requestsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(/*EventCreateBindingModel bindingModel*/string test)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Requests/Create");
            }

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            var requests = this.requestsService.GetAllRequests();
            return this.View(requests);
        }
    }
}
