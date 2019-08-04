namespace MyResourcePlanning.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Training;
    using MyResourcePlanning.Web.BindingModels.Training;
    using MyResourcePlanning.Web.ViewModels.Training;

    public class TrainingController : AdminController
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

            return this.Redirect("/Training/All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var categoryForUpdate = await this.trainingService.GetTrainingById(id);

            return this.View(categoryForUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TrainingEditBindingModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(await this.trainingService.GetTrainingById(id));
            }

            await this.trainingService.Edit(model, id);

            return this.Redirect("/Training/All");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.trainingService.Delete(id);

            return this.Redirect("/Training/All");
        }
    }
}
