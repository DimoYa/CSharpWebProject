namespace MyResourcePlanning.Web.Controllers
{
    using System;
    using System.Linq;
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

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.trainingService.Delete(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Request(string id)
        {
            var trainingToAdd = await this.trainingService.GetTrainingById(id);

            return this.View(trainingToAdd);
        }

        [HttpPost]
        public async Task<IActionResult> Request(TrainingRequestBindingModel inputModel, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel ?? new TrainingRequestBindingModel());
            }

            await this.trainingService.Request(id, inputModel);

            return this.RedirectToAction(nameof(this.MyTrainings));
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

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> ChangeUserTrainingStatus(string id)
        {
            var identifiers = SplitId(id, '_');

            var trainingId = identifiers[0];
            var userId = identifiers[1];

            var userTrainings = await this.trainingService.GetUserTrainingByIds<TrainingAllUsersView>(trainingId, userId);

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

        public async Task<IActionResult> MyTrainings()
        {
            var userTrainings = await this.trainingService
                .GetUserTrainings<TrainingUserViewModel>();

            return this.View(userTrainings);
        }

        public async Task<IActionResult> AllUsersTrainings()
        {
            var allUsersTrainings = await this.trainingService
                .GetAllUsersTrainings<TrainingAllUsersView>();

            return this.View(allUsersTrainings);
        }

        public async Task<IActionResult> All()
        {
            var trainings = await this.trainingService.GetAllTrainings<TrainingAllViewModel>();

            var currentUserTrainings = await this.trainingService.GetUserTrainingsId();

            return this.View(Tuple.Create(trainings.ToList(), currentUserTrainings));
        }

        private static string[] SplitId(string id, char splitter)
        {
            return id
                .Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
