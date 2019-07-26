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
            var trainigToAssign = await this.GetTrainingById(id);
            var currentUser = await this.userService.GetCurrentUserId();

            UserTraining userTraining = new UserTraining
            {
               UserId = currentUser,
               TrainingId = trainigToAssign.Id,
               Status = UserTrainingStatus.Requested,
            };

            this.context.UserTrainings.Add(userTraining);

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

        public async Task<IEnumerable<TViewModel>> GetAllTrainings<TViewModel>()
        {
            var trainings = this.context.Trainings
                .Where(t => t.IsDeleted == false)
                .OrderBy(t => t.Name)
                .To<TViewModel>()
                .ToList();

            return trainings;
        }

        public async Task<Training> GetTrainingById(string id)
        {
            var training = this.context.Trainings
                .SingleOrDefault(t => t.Id == id);

            return training;
        }
    }
}
