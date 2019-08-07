namespace MyResourcePlanning.Services.Data.Training
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Models;
    using MyResourcePlanning.Web.BindingModels.Training;

    public interface ITrainingService
    {
        Task<bool> Create(TrainingCreateBindingModel inputModel);

        Task<bool> Edit(TrainingEditBindingModel model, string id);

        Task<bool> Delete(string id);

        Task<bool> Request(string id, TrainingRequestBindingModel model);

        Task<bool> AssignToUser(string trainingId, TrainingAssignBindingModel model);

        Task<bool> ChangeUserTrainingStatus(TrainingStatusChangeBindingModel model, string trainingId, string userId);

        Task<IEnumerable<TViewModel>> GetAllTrainings<TViewModel>();

        Task<IEnumerable<TViewModel>> GetCurrentUserTrainings<TViewModel>();

        Task<IEnumerable<TViewModel>> GetAllUsersTrainings<TViewModel>();

        Task<TViewModel> GetUserTrainingByIds<TViewModel>(string trainingId, string userId);

        Task<IList<string>> GetCurrentUserTrainingsId();

        Task<Training> GetTrainingById(string id);

        Task<TrainingAssignBindingModel> GetTrainingAssignBaseModel(string trainingId);
    }
}
