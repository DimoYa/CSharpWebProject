namespace MyResourcePlanning.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Training;
    using MyResourcePlanning.Web.BindingModels.Training;
    using MyResourcePlanning.Web.ViewModels.Training;

    public class TrainingController : BaseController
    {
        private readonly ITrainingService trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            this.trainingService = trainingService;
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrainingCreateBindingModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new TrainingCreateBindingModel());
            }

            await this.trainingService.Create(inputModel);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var requests = await this.trainingService.GetAllTrainings<TrainingAllViewModel>();
            return this.View(requests);
        }
    }
}
