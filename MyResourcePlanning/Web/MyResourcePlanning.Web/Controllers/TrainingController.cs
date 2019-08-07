namespace MyResourcePlanning.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Common;
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

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> Request(string id)
        {
            var trainingToAdd = await this.trainingService.GetTrainingById(id);

            return this.View(trainingToAdd);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> Request(TrainingRequestBindingModel inputModel, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new TrainingRequestBindingModel());
            }

            await this.trainingService.Request(id, inputModel);

            return this.RedirectToAction(nameof(this.MyTrainings));
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName)]
        public async Task<IActionResult> MyTrainings()
        {
            var userTrainings = await this.trainingService
                .GetCurrentUserTrainings<TrainingUserViewModel>();

            return this.View(userTrainings);
        }

        [Authorize(Roles = GlobalConstants.ResourceRoleName + "," +
                           GlobalConstants.AdministratorRoleName + "," +
                           GlobalConstants.PlannerRoleName)]
        public async Task<IActionResult> All()
        {
            var trainings = await this.trainingService.GetAllTrainings<TrainingAllViewModel>();

            var currentUserTrainings = await this.trainingService.GetCurrentUserTrainingsId();

            return this.View(Tuple.Create(trainings.ToList(), currentUserTrainings));
        }
    }
}
