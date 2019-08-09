namespace MyResourcePlanning.Web.Areas.Planner.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyResourcePlanning.Services.Data.Training;
    using MyResourcePlanning.Web.BindingModels.Training;
    using MyResourcePlanning.Web.ViewModels.Training;

    public class TrainingController : PlannerController
    {
        private readonly ITrainingService trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            this.trainingService = trainingService;
        }

        public async Task<IActionResult> AssignToUser(string id)
        {
            var assignTrainingModel = await this.trainingService.GetTrainingAssignBaseModel(id);

            return this.View(assignTrainingModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignTouser(TrainingAssignBindingModel inputModel, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new TrainingAssignBindingModel());
            }

            await this.trainingService.AssignToUser(id, inputModel);

            return this.Redirect("/Training/All");
        }

        public async Task<IActionResult> ChangeUserTrainingStatus(string id)
        {
            var identifiers = SplitId(id, '_');

            var trainingId = identifiers[0];
            var userId = identifiers[1];

            var userTrainings = await this.trainingService.GetUserTrainingByIds<TrainingAllUsersViewModel>(trainingId, userId);

            return this.View(userTrainings);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserTrainingStatus(TrainingStatusChangeBindingModel model, string id)
        {
            var identifiers = SplitId(id, '_');

            var trainingId = identifiers[0];
            var userId = identifiers[1];

            if (!this.ModelState.IsValid)
            {
                return this.View(model ?? new TrainingStatusChangeBindingModel());
            }

            await this.trainingService.ChangeUserTrainingStatus(model, trainingId, userId);

            return this.RedirectToAction(nameof(this.AllUsersTrainings));
        }

        public async Task<IActionResult> AllUsersTrainings()
        {
            var allUsersTrainings = await this.trainingService
                .GetAllUsersTrainings<TrainingAllUsersViewModel>();

            return this.View(allUsersTrainings);
        }

        private static string[] SplitId(string id, char splitter)
        {
            return id
                .Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
