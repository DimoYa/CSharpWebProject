namespace MyResourcePlanning.Services.Data.Training
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyResourcePlanning.Data;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Models.Enums;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.BindingModels.Training;
    using MyResourcePlanning.Web.ViewModels.Training;
    using MyResourcePlanning.Web.ViewModels.User;

    public class TrainingService : ITrainingService
    {
        private readonly MyResourcePlanningDbContext context;
        private readonly IUserService userService;

        public TrainingService(
            MyResourcePlanningDbContext context,
            IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<bool> Create(TrainingCreateBindingModel model)
        {
            Training training = new Training
            {
                Name = model.Name,
                Type = model.Type,
                DueDate = model.DueDate,
                Status = model.Status,
            };

            this.context.Trainings.Add(training);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Edit(TrainingEditBindingModel model, string id)
        {
            var trainingForUpdate = await this.GetTrainingById(id);

            trainingForUpdate.Name = model.Name;
            trainingForUpdate.Type = model.Type;
            trainingForUpdate.Status = model.Status;
            trainingForUpdate.DueDate = model.DueDate;
            trainingForUpdate.ModifiedOn = DateTime.UtcNow;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var trainingForDelete = await this.GetTrainingById(id);

            trainingForDelete.IsDeleted = true;
            trainingForDelete.DeletedOn = DateTime.Now;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Request(string id, TrainingRequestBindingModel model)
        {
            var trainigToRequest = await this.GetTrainingById(id);
            var currentUser = await this.userService.GetCurrentUserId();

            UserTraining userTraining = new UserTraining
            {
                UserId = currentUser,
                TrainingId = trainigToRequest.Id,
                Status = UserTrainingStatus.Requested,
            };

            this.context.UserTrainings.Add(userTraining);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AssignToUser(string trainingId, TrainingAssignBindingModel model)
        {
            var trainigToAssign = await this.GetTrainingById(trainingId);

            var resourceName = model.Resource
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var firstName = resourceName[0];
            var lastName = resourceName[1];

            var userToAssign = await this.userService.GetUserByName(firstName, lastName);

            UserTraining userTraining = new UserTraining
            {
                UserId = userToAssign.Id,
                TrainingId = trainigToAssign.Id,
                Status = UserTrainingStatus.Assigned,
            };

            this.context.UserTrainings.Add(userTraining);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ChangeUserTrainingStatus(TrainingStatusChangeBindingModel model, string trainingId, string userId)
        {
            var userTrainingForUpdate = this.context.UserTrainings
                .SingleOrDefault(x => x.TrainingId == trainingId && x.UserId == userId);

            userTrainingForUpdate.Status = model.Status;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IList<string>> GetUserTrainingsId()
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            var userTrainingssId = this.context.UserTrainings
                .Where(u => u.UserId == currentUserId)
                .Select(t => t.TrainingId)
                .ToList();

            return userTrainingssId;
        }

        public async Task<IEnumerable<TViewModel>> GetUserTrainings<TViewModel>()
        {
            var currentUserId = await this.userService.GetCurrentUserId();

            var trainings = this.context.UserTrainings
                 .Where(d => d.Training.DueDate >= DateTime.Now)
                 .Where(ut => ut.UserId == currentUserId)
                 .Where(s => s.Training.IsDeleted == false)
                 .To<TViewModel>()
                 .ToList();

            return trainings;
        }

        public async Task<IEnumerable<TViewModel>> GetAllUsersTrainings<TViewModel>()
        {

            var trainings = this.context.UserTrainings
                 .Where(d => d.Training.DueDate >= DateTime.Now)
                 .Where(s => s.Training.IsDeleted == false)
                 .To<TViewModel>()
                 .ToList();

            return trainings;
        }

        public async Task<IEnumerable<TViewModel>> GetAllTrainings<TViewModel>()
        {
            var trainings = this.context.Trainings
                .Where(t => t.IsDeleted == false)
                .OrderBy(t => t.Name)
                .To<TViewModel>()
                .ToList();

            return trainings;
        }

        public async Task<TViewModel> GetUserTrainingByIds<TViewModel>(string trainingId, string userId)
        {
            var userTrainingToUpdate = this.context.UserTrainings
                .Where(x => x.UserId == userId && x.TrainingId == trainingId)
                .To<TViewModel>()
                .FirstOrDefault();

            return userTrainingToUpdate;
        }

        public async Task<Training> GetTrainingById(string id)
        {
            var training = this.context.Trainings
                .Where(t => t.Id == id)
                .SingleOrDefault();

            return training;
        }

        public async Task<TrainingAssignBindingModel> GetTrainingAssignBaseModel(string trainingId)
        {
            var trainingById = await this.GetTrainingById(trainingId);
            var allResources = await this.userService.GetAllActiveResources<UsersViewModel>();

            var resourcesToAssign = allResources
                .Where(x => x.Trainings.All(t => t != trainingById.Name))
                .ToList();

            return new TrainingAssignBindingModel()
            {
                Name = trainingById.Name,
                Resources = resourcesToAssign,
            };
        }
    }
}
