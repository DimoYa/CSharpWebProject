namespace MyResourcePlanning.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class RequestsController : BaseController
    {
        public RequestsController()
        {
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
            return this.View();
        }
    }
}
