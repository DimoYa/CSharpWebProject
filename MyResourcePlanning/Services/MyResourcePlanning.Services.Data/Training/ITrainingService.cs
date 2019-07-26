namespace MyResourcePlanning.Services.Data.Training
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyResourcePlanning.Models;
    using MyResourcePlanning.Web.BindingModels.Training;

    public interface ITrainingService
    {
        Task<IEnumerable<TViewModel>> GetAllTrainings<TViewModel>();

        Task<bool> Create(TrainingCreateBindingModel inputModel);

        Task<bool> Edit(TrainingEditBindingModel model, string id);

        Task<bool> Delete(string id);

        Task<bool> Request(string id, TrainingRequestBindingModel model);

        Task<IList<string>> GetUserTrainingsId();

        Task<Training> GetTrainingById(string id);
    }
}
